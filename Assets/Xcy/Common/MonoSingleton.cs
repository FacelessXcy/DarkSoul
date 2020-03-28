using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Common
{
	/// <summary>
	/// MonoBehaviour单例泛型类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MonoSingleton<T>: MonoBehaviour
	where T:MonoSingleton<T>
	{
		private static T _instance = null;
		public static T Instance 
		{ 
			get { 
				if (_instance == null) 
				{ 
					_instance = FindObjectOfType<T>();
					if (FindObjectsOfType<T>().Length > 1)
					{
						Debug.LogWarning("More than 1"); 
						return _instance;
					}
					if (_instance == null) 
					{ 
						var instanceName = typeof(T).Name; 
						Debug.LogFormat("Instance Name: {0}", instanceName); 
						var instanceObj = GameObject.Find(instanceName);
						if (!instanceObj) 
							instanceObj = new GameObject(instanceName);
						_instance = instanceObj.AddComponent<T>(); 
						DontDestroyOnLoad(instanceObj); //保证实例例不不会被释放
						Debug.LogFormat("Add New Singleton {0} in Game!", instanceName);
					}
					else
					{
						Debug.LogFormat("Already exist: {0}", _instance.name);
					}
				}
				return _instance;
			}
		}
		protected virtual void OnDestroy()
		{
			_instance = null;
		}

	}
}
