using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


namespace Xcy.Utility
{


	public class Exporter
	{

		private static string GeneratePackageName()
		{
			return "Xcy_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
		}

		public static void OpenInFolder(string path)
		{
			Application.OpenURL("file:///" + path);
		}
		public static void ExportPackage(string assetName, string fileName)
		{
			AssetDatabase.ExportPackage(assetName, fileName, ExportPackageOptions.Recurse);
		}

		[MenuItem("Xcy/导出UnityPackage %e", false, 1)]
		private static void MenuClicked()
		{

			ExportPackage("Assets/Xcy", GeneratePackageName() + ".unitypackage");
			OpenInFolder("");
		}
	}
}
