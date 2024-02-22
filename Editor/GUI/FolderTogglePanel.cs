using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityFolderTemplator.Editor.GUI
{
    public class FolderTogglePanel : VisualElement
    {
        public FolderTogglePanel(List<Folder> folders)
        {
            foreach (var folder in folders)
            {
                Add(new FolderView(folder));
            }
        }
    }
}