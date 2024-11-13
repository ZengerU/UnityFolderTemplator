using System.IO;
using UnityEngine.UIElements;

namespace UnityFolderTemplator.Editor.GUI
{
    public sealed class FileView : Toggle
    {
        readonly File _file;

        public FileView(File file)
        {
            _file = file;
            text = string.Format(Path.GetFileName(file.Target ), "<folder_name>");
            value = file.IsEnabled;
            this.RegisterValueChangedCallback(OnValueChanged);
        }

        void OnValueChanged(ChangeEvent<bool> evt)
        {
            _file.IsEnabled = evt.newValue;
        }

        public void UpdateName(string s)
        {
            text = string.Format(Path.GetFileName(_file.Target ), s);
        }
    }
}