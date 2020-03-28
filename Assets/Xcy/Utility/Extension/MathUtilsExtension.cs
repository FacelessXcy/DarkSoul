using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Utility.Extension
{
	/// <summary>
	/// Math方法扩展
	/// </summary>
	public class MathUtilsExtension
	{
		/// <summary>
		/// 获取概率
		/// </summary>
		/// <param name="percent">所需概率</param>
		/// <returns>是否在概率范围内</returns >
		public static bool Percent(int percent)
		{
			return Random.Range(0, 100) < percent;
		}
		/// <summary>
		/// 随机获取一个元素
		/// </summary>
		/// <param name="values">样本</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetRandomValueFrom<T>(params T[] values)
		{
			return values[Random.Range(0, values.Length)];
		}

	}
}
