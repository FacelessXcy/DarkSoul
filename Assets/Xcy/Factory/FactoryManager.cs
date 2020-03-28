using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Factory.AssetLoad;


namespace Xcy.Factory
{
	public static class FactoryManager
	{
		private static IAssetFactory _assetFactory = null;

		public static IAssetFactory AssetFactory
		{
			get
			{
				if (_assetFactory == null)
				{
					_assetFactory = new ResourcesAssetFactory();
				}

				return _assetFactory;
			}
		}
	}
}