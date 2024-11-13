using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace UnityFolderTemplator.Editor
{
    public class AssemblyDefinitionFile : File
    {
        public AssemblyDefinitionFile(string source, string target, bool defaultValue, bool isAsmdef) : base(source,
            target, defaultValue, isAsmdef)
        {
        }

        public override void Copy(string name, string currentPath)
        {
            var fileName = string.Format(Target, name);
            var location = Path.Combine(currentPath, fileName);
            var path = Path.Combine(Path.GetDirectoryName(Application.dataPath)!, Source);
            CopyAsmdefFile(System.IO.File.ReadAllText(path), location);
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