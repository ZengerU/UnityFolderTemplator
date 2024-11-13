using System.IO;

namespace UnityFolderTemplator.Editor
{
    public class CsFile : File
    {
        public CsFile(string source, string target, bool defaultValue, bool isAsmdef) : base(source, target, defaultValue, isAsmdef)
        {
        }

        public override void Copy(string name, string path)
        {
            var text = System.IO.File.ReadAllText(Creator.ConvertAssetDatabasePathToAbsolute(Source));
            text = text.Replace("{0}", name);
            path = Path.Combine(path, string.Format(Target, name));
            System.IO.File.WriteAllText(Creator.ConvertAssetDatabasePathToAbsolute(path), text);
        }
    }
}