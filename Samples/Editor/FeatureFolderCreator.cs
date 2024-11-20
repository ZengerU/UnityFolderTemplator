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

        const string AsmdefRootLocation = "Assets/Anonymouse/FolderTemplator/Samples/Templates/";
        static string EditorAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Editor.template");
        static string RuntimeAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.template");
        static string TestEditorAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Editor.Tests.template");
        static string TestRuntimeAsmdefLocation => Path.Combine(AsmdefRootLocation, "Feature.Tests.template");

        protected override Folder Folder { get; } = new("{0}", true, new List<Folder>
        {
            new("ArtAssets", true, new List<Folder>
            {
                new("Audio", false),
                new("Image", false),
                new("Video", false)
            }),
            new("Data", true),
            new("Editor", true, files: new List<File>
            {
                new AssemblyDefinitionFile(EditorAsmdefLocation, "{0}.Editor.asmdef", true, true)
            }),
            new("Features", false),
            new("Prefabs", true),
            new("Runtime", true, files: new List<File>
            {
                new AssemblyDefinitionFile(RuntimeAsmdefLocation, "{0}.asmdef", true, true)
            }),
            new("Tests", true, new List<Folder>
            {
                new("Editor", true, files: new List<File>
                {
                    new AssemblyDefinitionFile(TestEditorAsmdefLocation, "{0}.Tests.Editor.asmdef", true, true)
                }),
                new("Runtime", true, files: new List<File>
                {
                    new AssemblyDefinitionFile(TestRuntimeAsmdefLocation, "{0}.Tests.asmdef", true, true)
                })
            }),
        });
    }
}