using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Xcy.Factory.AssetLoad
{
	public class ResourcesAssetFactory : IAssetFactory
	{
		public const string PrefabPath = "Prefab/";
		public const string EffectPath = "Effects/";
		public const string ClipPath = "Audios/Clips";
		public const string MusicPath = "Audios/Musics";
		public const string SpritePath = "Sprites/";

		public GameObject LoadPrefab(string name)
		{
			GameObject gameObject = Resources.Load<GameObject>(PrefabPath + name);
			if (gameObject == null)
			{
				Debug.LogError("资源加载失败：" + PrefabPath + name);
				return null;
			}

			return gameObject;
		}

		public GameObject LoadEffect(string name)
		{
			GameObject gameObject = Resources.Load<GameObject>(EffectPath + name);
			if (gameObject == null)
			{
				Debug.LogError("资源加载失败：" + EffectPath + name);
				return null;
			}

			return gameObject;
		}

		public AudioClip LoadAudioClip(string name)
		{
			AudioClip clip = Resources.Load<AudioClip>(ClipPath + name);
			if (clip == null)
			{
				Debug.LogError("资源加载失败：" + ClipPath + name);
				return null;
			}

			return clip;
		}

		public AudioClip LoadMusic(string name)
		{
			AudioClip clip = Resources.Load<AudioClip>(MusicPath + name);
			if (clip == null)
			{
				Debug.LogError("资源加载失败：" + MusicPath + name);
				return null;
			}

			return clip;
		}

		public Sprite LoadSprite(string name)
		{
			Sprite sprite = Resources.Load<Sprite>(SpritePath + name);
			if (sprite == null)
			{
				Debug.LogError("资源加载失败：" + SpritePath + name);
				return null;
			}

			return sprite;
		}
	}
}
