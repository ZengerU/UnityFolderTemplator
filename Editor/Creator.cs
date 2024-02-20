using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FolderCreator.Editor.GUI;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FolderCreator.Editor
{
    public abstract class Creator : EditorWindow
    {
        string _path;
        TextField _folderNameField;

        protected abstract List<Folder> Folders { get; }

        void CreateGUI()
        {
            var obj = Selection.activeObject;
            _path = AssetDatabase.GetAssetPath(obj);
            
            var guids = AssetDatabase.FindAssets("FolderCreatorStyle");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(guids[0]));
            rootVisualElement.styleSheets.Add(styleSheet);
            
            _folderNameField = new TextField
            {
                label = "Folder Name:",
                tooltip = "Name of the folder. Note: This is also used in asmdef names."
            };
            rootVisualElement.Add(_folderNameField);
            
            var scroll = new ScrollView();
            rootVisualElement.Add(scroll);
            var togglePanel = new FolderTogglePanel(Folders);
            scroll.Add(togglePanel);
            var createButton = new Button(Create)
            {
                text = "Create"
            };
            rootVisualElement.Add(createButton);
        }

        void Create()
        {
            if (!Directory.Exists(_path))
                _path = Path.GetDirectoryName(_path);
            var rootName = _folderNameField.value;

            AssetDatabase.StartAssetEditing();

            try
            {
                AssetDatabase.CreateFolder(_path, rootName);

                _path = Path.Combine(_path!, rootName);

                foreach (var folder in Folders.Where(x => x.IsEnabled))
                {
                    CreateFolder(folder, _path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
                Close();
            }
        }
        void CreateFolder(Folder folder, string currentPath)
        {
            AssetDatabase.CreateFolder(currentPath, folder.Name);

            foreach (var file in folder.Files.Where(x => x.IsEnabled))
            {
                CopyFile(file, Path.Combine(currentPath, folder.Name));
            }

            foreach (var childFolder in folder.ChildFolders.Where(x => x.IsEnabled))
            {
                CreateFolder(childFolder, Path.Combine(currentPath, folder.Name));
            }
        }

        void CopyFile(File file, string currentPath)
        {
            var fileName = string.Format(file.Target, _folderNameField.value);
            var location = Path.Combine(currentPath, fileName);
            if (file.IsAsmdef)
            {
                var path = Path.Combine(Path.GetDirectoryName(Application.dataPath), file.Source);
                CopyAsmdefFile(System.IO.File.ReadAllText(path), location);
            }
            else
            {
                AssetDatabase.CopyAsset(file.Source, location);
            }
        }

        static void CopyAsmdefFile(string source, string location)
        {
            var content = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(source);
            content["name"] = Path.GetFileNameWithoutExtension(location);
            var result = JsonConvert.SerializeObject(content);
            System.IO.File.WriteAllText(ConvertAssetDatabasePathToAbsolute(location), result);
        }

        static string ConvertAssetDatabasePathToAbsolute(string path)
        {
            var projectPath = Application.dataPath;
            projectPath = Directory.GetParent(projectPath)!.FullName;
            return Path.Combine(projectPath, path);
        }
    }
}