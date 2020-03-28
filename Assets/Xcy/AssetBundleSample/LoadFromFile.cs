//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.VersionControl;
//using UnityEngine;
//using UnityEngine.Networking;
//using Object = UnityEngine.Object;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//
//public class LoadFromFile : MonoBehaviour
//{
//	IEnumerator Start()
//	{
//		string path=@"file:\F:/Game(Work)/project/AsssetBundle/AssetBundles/wallcap.u3d";
////		AssetBundle assetBundleDepend=AssetBundle.LoadFromFile("AssetBundles/share.u3d");
////		AssetBundle ab = AssetBundle.LoadFromFile("AssetBundles/wallcap.u3d");
////		GameObject wallPrefab= ab.LoadAsset<GameObject>("Wall");
////		Object[] prefabs = ab.LoadAllAssets<GameObject>();
////		foreach (Object prefab in prefabs)
////		{
////			GameObject.Instantiate(prefab);
////		}
//		
//		//第一种加载AB的方式 LoadFromMemoryAsync
////		AssetBundleCreateRequest request= AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
////		yield return request;
////		AssetBundle ab = request.assetBundle;
//		//LoadFromMemory
//		//AssetBundle ab= AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
//		
//		//第二种加载AB的方式 LoadFromFile
////		AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
////		yield return request;
////		AssetBundle ab = request.assetBundle;
//		
//		//第三种加载AB的方式 WWW  
//		//file:\\  file:/  file://
////		while (!Caching.ready)
////		{
////			yield return null;
////		}
//
////		WWW www= WWW.LoadFromCacheOrDownload(path, 1);
////		WWW www = WWW.LoadFromCacheOrDownload(@"http://localhost/AssetBundles/wallcap.u3d", 1);
////		yield return www;
////		if (www.error!=null)
////		{
////			Debug.Log(www.error);
////			yield break; 
////		}
////		AssetBundle ab = www.assetBundle;
//
//		//第四种加载AB的方式 UnityWebRequest
//		string uri = path;
//		UnityWebRequest request= UnityWebRequestAssetBundle.GetAssetBundle(uri);
//		yield return request.SendWebRequest();
//		//AssetBundle ab= DownloadHandlerAssetBundle.GetContent(request);
//		AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
//		//使用资源
//		GameObject wallPrefab= ab.LoadAsset<GameObject>("WallCap");
//		GameObject.Instantiate(wallPrefab);
//		AssetBundle manifestAB=AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
//		AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
//
//		foreach (string name in manifest.GetAllAssetBundles())
//		{
//			Debug.Log(name);
//		}
//		string[] strs= manifest.GetAllDependencies("wallcap.u3d");
//		foreach (string str in strs)
//		{
//			Debug.Log(str);
//			AssetBundle.LoadFromFile("AssetBundles/" + str);
//		}
//		
//	}
//}
