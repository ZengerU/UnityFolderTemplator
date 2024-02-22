using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityFolderTemplator.Editor;
using File = UnityFolderTemplator.Editor.File;

namespace UnityFolderTemplator.Samples.Editor
{
    public class FeatureFolderCreator : Creator
    {
        [MenuItem("Assets/Create/Folder Templates/Feature", false, 19)]
        public static void ShowMyEditor()
        {
            EditorWindow wnd = GetWindow<FeatureFolderCreator>(true, "Feature Folder Creator", true);
            wnd.maxSize = new Vector2(400, 600);
        }

        const string AsmdefRootLocation = "Assets/FolderCreator/Samples/Templates/";
        static string EditorAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Editor.template");
        static string RuntimeAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.template");
        static string TestEditorAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Editor.Tests.template");
        static string TestRuntimeAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Tests.template");


        protected override List<Folder> Folders { get; } = new()
        {
            new Folder("ArtAssets", true, new List<Folder>
            {
                new("Audio", false),
                new("Image", false),
                new("Video", false)
            }),
            new Folder("Data", true),
            new Folder("Editor", true, files: new List<File>
            {
                new(EditorAsmdefLocation, "{0}.Editor.asmdef", true, true)
            }),
            new Folder("Features", false),
            new Folder("Prefabs", true),
            new Folder("Runtime", true, files: new List<File>
            {
                new(RuntimeAsmdefLocation, "{0}.asmdef", true, true)
            }),
            new Folder("Tests", true, new List<Folder>
            {
                new("Editor", true, files: new List<File>
                {
                    new(TestEditorAsmdefLocation, "{0}.Tests.Editor.asmdef", true, true)
                }),
                new("Runtime", true, files: new List<File>
                {
                    new(TestRuntimeAsmdefLocation, "{0}.Tests.asmdef", true, true)
                })
            }),
        };
    }
}