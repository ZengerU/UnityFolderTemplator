using UnityEngine.UIElements;

namespace UnityFolderTemplator.Editor.GUI
{
    public class FolderView : VisualElement
    {
        readonly Folder _folder;
        readonly Toggle _toggle;
        VisualElement _content;

        public FolderView(Folder folder)
        {
            _folder = folder;
            _toggle = new Toggle
            {
                text = folder.Name,
                value = folder.IsEnabled
            };
            _toggle.RegisterValueChangedCallback(OnValueChanged);
            Add(_toggle);
            _content = new VisualElement();
            Add(_content);
            
            foreach (var childFolder in folder.ChildFolders)
            {
                _content.Add(new FolderView(childFolder));
            }
            foreach (var folderFile in folder.Files)
            {
                _content.Add(new FileView(folderFile));
            }
        }

        void OnValueChanged(ChangeEvent<bool> evt)
        {
            _folder.IsEnabled = evt.newValue;
        }

        public void UpdateName(string s)
        {
            _toggle.text = s;
            foreach (var child in _content.Children())
            {
                if(child is FolderView folderView)
                    folderView.UpdateName(s);
                if(child is FileView fileView)
                    fileView.UpdateName(s);
            }
            foreach (var child in _folder.ChildFolders)
            {
                
            }
        }
    }
}