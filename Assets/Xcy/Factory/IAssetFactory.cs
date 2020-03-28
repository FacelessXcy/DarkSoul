using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAssetFactory
{

	GameObject LoadPrefab(string name);
	GameObject LoadEffect(string name);
	AudioClip LoadAudioClip(string name);
	AudioClip LoadMusic(string name);
	Sprite LoadSprite(string name);
} 
