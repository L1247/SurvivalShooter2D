//-----------------------------------------------------------------------
// <copyright file="BuildAOTAutomation.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#region

using System.IO;
using UnityEditor;
using UnityEditor.Build;

#endregion

#if UNITY_EDITOR && UNITY_5_6_OR_NEWER

namespace Sirenix.Serialization.Internal
{
#region

#region

#if UNITY_2018_1_OR_NEWER
    using UnityEditor.Build.Reporting;

#endif

#endregion

#endregion

#if UNITY_2018_1_OR_NEWER
    public class PreBuildAOTAutomation : IPreprocessBuildWithReport
#else
    public class PreBuildAOTAutomation : IPreprocessBuild
#endif
    {
        public int callbackOrder => -1000;

        public void OnPreprocessBuild(BuildTarget target , string path)
        {
            if (AOTGenerationConfig.Instance.ShouldAutomationGeneration(target))
            {
                AOTGenerationConfig.Instance.ScanProject();
                AOTGenerationConfig.Instance.GenerateDLL();
            }
        }

    #if UNITY_2018_1_OR_NEWER

        public void OnPreprocessBuild(BuildReport report)
        {
            OnPreprocessBuild(report.summary.platform , report.summary.outputPath);
        }

    #endif
    }

#if UNITY_2018_1_OR_NEWER
    public class PostBuildAOTAutomation : IPostprocessBuildWithReport
#else
    public class PostBuildAOTAutomation : IPostprocessBuild
#endif
    {
        public int callbackOrder => -1000;

        public void OnPostprocessBuild(BuildTarget target , string path)
        {
            if (AOTGenerationConfig.Instance.DeleteDllAfterBuilds &&
                AOTGenerationConfig.Instance.ShouldAutomationGeneration(target))
            {
                Directory.Delete(AOTGenerationConfig.Instance.AOTFolderPath , true);
                File.Delete(AOTGenerationConfig.Instance.AOTFolderPath.TrimEnd('/' , '\\') + ".meta");
                AssetDatabase.Refresh();
            }
        }

    #if UNITY_2018_1_OR_NEWER

        public void OnPostprocessBuild(BuildReport report)
        {
            OnPostprocessBuild(report.summary.platform , report.summary.outputPath);
        }

    #endif
    }
}

#endif // UNITY_EDITOR && UNITY_5_6_OR_NEWER