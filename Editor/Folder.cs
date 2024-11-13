using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UnityFolderTemplator.Editor
{
    public class Folder
    {
        public readonly string Name;
        public readonly List<Folder> ChildFolders;
        public readonly List<File> Files; 
        public bool IsEnabled;

        public Folder(string name, bool defaultValue, List<Folder> childFolders = default, List<File> files = default)
        {
            Name = name;
            IsEnabled = defaultValue;
            ChildFolders = childFolders ?? new List<Folder>();
            Files = files ?? new List<File>();
        }

        public void Create(string path, string name)
        {
            var folderName = string.Format(Name, name);
            AssetDatabase.CreateFolder(path, folderName);
            
            path = System.IO.Path.Combine(path, folderName);
            
            foreach (var file in Files.Where(x => x.IsEnabled))
            {
                file.Copy(name, path);
            }
            foreach (var folder in ChildFolders.Where(x => x.IsEnabled))
            {
                folder.Create(path, name);
            }
        }
    }
}