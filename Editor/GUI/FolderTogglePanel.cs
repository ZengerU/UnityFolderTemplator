using UnityEngine.UIElements;

namespace UnityFolderTemplator.Editor.GUI
{
    public class FolderTogglePanel : VisualElement
    {
        readonly FolderView _folderView;

        public FolderTogglePanel(Folder rootFolder)
        {
            _folderView = new FolderView(rootFolder);
            Add(_folderView);
        }
        
        public void UpdateName(string name) => _folderView.UpdateName(name);
    }
}