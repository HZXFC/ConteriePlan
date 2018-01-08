using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ConteriePlan
{
    public class BuildAssetBundle : UnityEditor.Editor
    {
        [MenuItem("Custom/AssetBundles/创建AssetBundles")]
        public static void Build()
        {
            BuildPipeline.BuildAssetBundles("Assets/Resources", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

    }
}

