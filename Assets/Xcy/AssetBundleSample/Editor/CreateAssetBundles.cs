using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;


public class CreateAssetBundles
{
	[MenuItem("AssetBundle/BuildAssetBundle")]
	static void BuildAllAssetBundle()
	{
		string dir = "AssetBundles";
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}
		BuildPipeline.BuildAssetBundles(dir, 
			BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

	}

}
