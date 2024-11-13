using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityFolderTemplator.Editor
{
    public class File
    {
        public readonly string Source;
        public readonly string Target;
        public bool IsEnabled;
        public readonly bool IsAsmdef;

        public File(string source, string target, bool defaultValue, bool isAsmdef)
        {
            Source = source;
            Target = target;
            IsEnabled = defaultValue;
            IsAsmdef = isAsmdef;
        }

        public virtual void Copy(string name, string path)
        {
            var fileName = string.Format(Target, name);
            var location = Path.Combine(path, fileName);
            AssetDatabase.CopyAsset(Source, location);
        }
    }
}