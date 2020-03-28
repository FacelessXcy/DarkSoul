using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcy.Utility
{
	public class ResolutionCheck
	{
		public static float GetAspectRatio()
		{
			var isLandScap = Screen.width > Screen.height;
			return isLandScap ? (float) Screen.width / Screen.height : (float) Screen.height / Screen.width;
		}

		public static bool IsPadResolution()
		{
			return InAspectRange(4.0f / 3);
		}

		public static bool IsPhoneResolution()
		{
			return InAspectRange(16.0f / 9);
		}

		public static bool IsPhone15Resolution()
		{
			return InAspectRange(3.0f / 2);
		}

		public static bool IsPhoneXResolution()
		{
			return InAspectRange(2436.0f / 1125);
		}

		public static bool InAspectRange(float dstAspectRatio)
		{
			var aspect = GetAspectRatio();
			return (aspect > (dstAspectRatio - 0.05f) && aspect < (dstAspectRatio + 0.05f)) ? true : false;
		}

	}
}
