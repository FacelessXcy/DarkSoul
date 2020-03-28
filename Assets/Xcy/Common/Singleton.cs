using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace Xcy.Common
{
	/// <summary>
	/// 常规单例泛型类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Singleton<T>
	where T:Singleton<T>
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance==null)
				{
					//无法new，使用反射创建对象
					ConstructorInfo[] creators = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
					ConstructorInfo creator = Array.Find(creators, c => c.GetParameters().Length == 0);
					if (creator==null)
						throw  new  Exception("非公有构造函数未找到");
					//调用由具有指定参数的实例反映的构造函数，为不常用的参数提供默认值。
					_instance = creator.Invoke(null) as T;
				}
				return _instance;
			}
		}

		protected Singleton()
		{
			
		}
	}
}
