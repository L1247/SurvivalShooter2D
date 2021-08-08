//-----------------------------------------------------------------------
// <copyright file="AssemblyImportSettingsAutomation.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#region

using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization.Utilities.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Build;

#endregion

#if UNITY_EDITOR && UNITY_5_6_OR_NEWER

namespace Sirenix.OdinInspector.Editor
{
#region

#region

#if UNITY_2018_1_OR_NEWER
    using UnityEditor.Build.Reporting;
#endif

#endregion

#endregion

    public class AssemblyImportSettingsAutomation :
    #if UNITY_2018_1_OR_NEWER
        IPreprocessBuildWithReport
#else
        IPreprocessBuild
#endif
    {
        public int callbackOrder => -1500;

        private static void ConfigureImportSettings()
        {
            if (EditorOnlyModeConfig.Instance.IsEditorOnlyModeEnabled() ||
                ImportSettingsConfig.Instance.AutomateBeforeBuild == false) return;

            var assemblyDir       = new DirectoryInfo(SirenixAssetPaths.SirenixAssembliesPath).FullName;
            var projectAssetsPath = Directory.GetCurrentDirectory().TrimEnd('\\' , '/');

            var isPackage = new DirectoryInfo(projectAssetsPath).HasSubDirectory(new DirectoryInfo(assemblyDir)) ==
                            false;

            var aotDirPath = assemblyDir + "NoEmitAndNoEditor/";
            var jitDirPath = assemblyDir + "NoEditor/";

            var aotDir = new DirectoryInfo(aotDirPath);
            var jitDir = new DirectoryInfo(jitDirPath);

            var aotAssemblies = new List<string>();
            var jitAssemblies = new List<string>();

            foreach (var file in aotDir.GetFiles("*.dll"))
            {
                var path = file.FullName;
                if (isPackage)
                    path = SirenixAssetPaths.SirenixAssembliesPath.TrimEnd('\\' , '/') + "/" +
                           path.Substring(assemblyDir.Length);
                else
                    path = path.Substring(projectAssetsPath.Length + 1);

                aotAssemblies.Add(path);
            }

            foreach (var file in jitDir.GetFiles("*.dll"))
            {
                var path = file.FullName;
                if (isPackage)
                    path = SirenixAssetPaths.SirenixAssembliesPath.TrimEnd('\\' , '/') + "/" +
                           path.Substring(assemblyDir.Length);
                else
                    path = path.Substring(projectAssetsPath.Length + 1);

                jitAssemblies.Add(path);
            }

            AssetDatabase.StartAssetEditing();
            try
            {
                var platform = EditorUserBuildSettings.activeBuildTarget;

                if (AssemblyImportSettingsUtilities.IsJITSupported(
                    platform ,
                    AssemblyImportSettingsUtilities.GetCurrentScriptingBackend() ,
                    AssemblyImportSettingsUtilities.GetCurrentApiCompatibilityLevel()))
                {
                    ApplyImportSettings(platform , aotAssemblies.ToArray() , OdinAssemblyImportSettings.ExcludeFromAll);
                    ApplyImportSettings(platform , jitAssemblies.ToArray() ,
                                        OdinAssemblyImportSettings.IncludeInBuildOnly);
                }
                else
                {
                    ApplyImportSettings(platform , aotAssemblies.ToArray() ,
                                        OdinAssemblyImportSettings.IncludeInBuildOnly);
                    ApplyImportSettings(platform , jitAssemblies.ToArray() , OdinAssemblyImportSettings.ExcludeFromAll);
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        private static void ApplyImportSettings(BuildTarget                platform , string[] assemblyPaths ,
                                                OdinAssemblyImportSettings importSettings)
        {
            for (var i = 0 ; i < assemblyPaths.Length ; i++)
                AssemblyImportSettingsUtilities.SetAssemblyImportSettings(platform , assemblyPaths[i] , importSettings);
        }

    #if UNITY_2018_1_OR_NEWER

        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
        {
            ConfigureImportSettings();
        }

    #else
        void IPreprocessBuild.OnPreprocessBuild(BuildTarget target, string path)
        {
            ConfigureImportSettings();
        }

    #endif
    }
}

#endif // UNITY_EDITOR && UNITY_5_6_OR_NEWER