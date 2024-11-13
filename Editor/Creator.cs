using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityFolderTemplator.Editor.GUI;

namespace UnityFolderTemplator.Editor
{
    public abstract class Creator : EditorWindow
    {
        string _path;
        TextField _folderNameField;
        FolderTogglePanel _togglePanel;

        protected abstract Folder Folder { get; }

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
            _folderNameField.RegisterValueChangedCallback(FolderNameChanged);
            rootVisualElement.Add(_folderNameField);

            var scroll = new ScrollView();
            rootVisualElement.Add(scroll);
            _togglePanel = new FolderTogglePanel(Folder);
            scroll.Add(_togglePanel);
            var createButton = new Button(Create)
            {
                text = "Create"
            };
            rootVisualElement.Add(createButton);
        }

        void FolderNameChanged(ChangeEvent<string> evt)
        {
            _togglePanel.UpdateName(evt.newValue);
        }

        void Create()
        {
            if (!Directory.Exists(_path))
                _path = Path.GetDirectoryName(_path);

            AssetDatabase.StartAssetEditing();

            try
            {
                Folder.Create(_path, _folderNameField.value);
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
        public static string ConvertAssetDatabasePathToAbsolute(string path)
        {
            var projectPath = Application.dataPath;
            projectPath = Directory.GetParent(projectPath)!.FullName;
            return Path.Combine(projectPath, path);
        }
    }
}