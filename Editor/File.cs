namespace FolderCreator.Editor
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
    }
}