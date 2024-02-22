using System.Collections.Generic;

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
    }
}