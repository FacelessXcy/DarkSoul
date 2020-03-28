using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource _musicSource;
    private Dictionary<string,AudioSource> _soundSources=new Dictionary<string, AudioSource>();
	private AudioListener _audioListener;

        private void CheckAudioListener()
        {
            if (!_audioListener)
            {
                _audioListener = FindObjectOfType<AudioListener>();
            }

            if (!_audioListener)
            {
                _audioListener = gameObject.AddComponent<AudioListener>();
            }
        }
   
        public void PlaySound(string soundName)
        {
            CheckAudioListener();
            if (!_soundSources.ContainsKey(soundName))
            {
                AudioClip clip = Resources.Load<AudioClip>(soundName);
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                _soundSources.Add(soundName,audioSource);
            }
            _soundSources[soundName].Play();
        }


        public void PlayMusic(string musicName, bool loop)
        {
            CheckAudioListener();
            if (!_musicSource)
            {
                _musicSource = gameObject.AddComponent<AudioSource>();
            }
            if (_musicSource.clip.name!=musicName)
            {
                AudioClip clip = Resources.Load<AudioClip>(musicName);
                _musicSource.clip = clip;
            }
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void PauseMusic()
        {
            _musicSource.Pause();
        }

        public void ResumeMusic()
        {
            _musicSource.UnPause();
        }

        public void MusicOff()
        {
            _musicSource.Pause();
            _musicSource.mute = true;
        }

        public void SoundOff()
        {
            foreach(AudioSource soundSource in _soundSources.Values)
            {
                soundSource.Pause();
                soundSource.mute = true;
            }
        }

        public void MusicOn()
        {
            _musicSource.UnPause();
            _musicSource.mute = false;
        }

        public void SoundOn()
        {
            foreach (var soundSource in _soundSources.Values)
            {
                soundSource.UnPause();
                soundSource.mute = false;
            }
        }
	
}
