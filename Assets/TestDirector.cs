using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestDirector : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    public Animator attacker;
    public Animator victim;

    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        //Debug.Log(_playableDirector==null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (PlayableBinding track in _playableDirector.playableAsset.outputs)
            {
                //Debug.Log(track.streamName);
                if (track.streamName=="Attacker Animation")
                {
                    _playableDirector.SetGenericBinding(track.sourceObject,
                        attacker); 
                }else if (track.streamName=="Victim Animation")
                {
                    _playableDirector.SetGenericBinding(track.sourceObject,
                                        victim); 
                }
            }

                _playableDirector.time = 0;
            _playableDirector.Stop();
            //_playableDirector.Evaluate();
            _playableDirector.Play();
        }
    }
}
