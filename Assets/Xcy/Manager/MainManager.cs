using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Manager
{
	public enum EnviromentMode
	{
		Developing,
		Test,
		Production
	}

	public abstract class MainManager : MonoBehaviour
	{
		public EnviromentMode mode;

		private static EnviromentMode _sharedMode;
		private static bool _modeSetted = false;

		private void Start()
		{
			if (!_modeSetted)
			{
				_sharedMode = mode;
				_modeSetted = true;
			}
			switch (_sharedMode)
			{
				case EnviromentMode.Developing:
					LaunchInDevelopingMode();
					break;
				case EnviromentMode.Production:
					LaunchInProductionMode();
					break;
				case EnviromentMode.Test:
					LaunchInTestMode();
					break;
			}
		}

		protected abstract void LaunchInDevelopingMode();
		protected abstract void LaunchInProductionMode();
		protected abstract void LaunchInTestMode();

	}
}
