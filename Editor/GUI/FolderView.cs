using UnityEngine.UIElements;

namespace UnityFolderTemplator.Editor.GUI
{
    public class FolderView : VisualElement
    {
        readonly Folder _folder;

        public FolderView(Folder folder)
        {
            _folder = folder;
            var toggle = new Toggle
            {
                text = folder.Name,
                value = folder.IsEnabled
            };
            toggle.RegisterValueChangedCallback(OnValueChanged);
            Add(toggle);
            var content = new VisualElement();
            Add(content);
            
            foreach (var childFolder in folder.ChildFolders)
            {
                content.Add(new FolderView(childFolder));
            }
            foreach (var folderFile in folder.Files)
            {
                content.Add(new FileView(folderFile));
            }
        }

        void OnValueChanged(ChangeEvent<bool> evt)
        {
            _folder.IsEnabled = evt.newValue;
        }
    }
}