using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector pd;

    [Header("=== Timeline Asset ===")] 
    public TimelineAsset frontStab;
    public TimelineAsset openBox;
    public TimelineAsset leverUp;

    [Header("=== Assets Settings ===")] 
    public ActorManager attacker;
    public ActorManager victim;
    
    private void Start()
    {
        pd = GetComponent<PlayableDirector>(); 
        pd.playOnAwake = false;


    }

    public void PlayFrontStab(string timelineName,ActorManager 
    attacker,ActorManager victim)
    {
//        if (pd.playableAsset!=null)
//        {
//            return;
//        }
        if (pd.state==PlayState.Playing)
        {
            return;
        }
        if (timelineName=="frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);
            //获取剧本文件
            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;
            
            //pd.Evaluate();
            //获取每个轨道
            foreach (TrackAsset track in timelineAsset.GetOutputTracks())
            {
                //Debug.Log(outputTrack.name);
                switch (track.name)
                {
                    case "Attacker Script":
                        pd.SetGenericBinding(track,attacker);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            MySuperPlayableBehaviour
                                myBehavior = myClip.template;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                            .exposedName,attacker);
                            //不会报错，但是并不能绑定
                            //myBehavior.myCamera = GameObject.Find("A");
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Victim Script":
                        pd.SetGenericBinding(track,victim);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            MySuperPlayableBehaviour
                                myBehavior = myClip.template;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                                .exposedName,victim);
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Attacker Animation":
                        pd.SetGenericBinding(track,attacker.ac.Anim);
                        break;
                    case "Victim Animation":
                        pd.SetGenericBinding(track,victim.ac.Anim);
                        break;
                }
            }
            pd.Evaluate();
            pd.Play();
        }
        else if (timelineName=="openBox")
        {
            pd.playableAsset = Instantiate(openBox);
            //获取剧本文件 
            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;
            //pd.Evaluate();
            //获取每个轨道
            foreach (TrackAsset track in timelineAsset.GetOutputTracks())
            {
                //Debug.Log(outputTrack.name);
                switch (track.name)
                {
                    case "Player Script":
                        pd.SetGenericBinding(track,attacker);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                            .exposedName,attacker);
                            //不会报错，但是并不能绑定
                            //myBehavior.myCamera = GameObject.Find("A");
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Box Script":
                        pd.SetGenericBinding(track,victim);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                                .exposedName,victim);
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Player Animation":
                        pd.SetGenericBinding(track,attacker.ac.Anim);
                        break;
                    case "Box Animation":
                        pd.SetGenericBinding(track,victim.ac.Anim);
                        break;
                }
            }
            pd.Evaluate();
            pd.Play();
        }
        else if (timelineName=="leverUp")
        {
             pd.playableAsset = Instantiate(leverUp);
            //获取剧本文件 
            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;
            //pd.Evaluate();
            //获取每个轨道
            foreach (TrackAsset track in timelineAsset.GetOutputTracks())
            {
                //Debug.Log(outputTrack.name);
                switch (track.name)
                {
                    case "Player Script":
                        pd.SetGenericBinding(track,attacker);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                            .exposedName,attacker);
                            //不会报错，但是并不能绑定
                            //myBehavior.myCamera = GameObject.Find("A");
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Lever Script":
                        pd.SetGenericBinding(track,victim);
                        foreach (TimelineClip clip in track.GetClips())
                        {
                            MySuperPlayableClip myClip = 
                                (MySuperPlayableClip)clip.asset;
                            //初始化exposedName的key值
                            myClip.am.exposedName=System.Guid.NewGuid().ToString(); 
                            pd.SetReferenceValue(myClip.am
                                .exposedName,victim);
                            //Debug.Log(myBehavior.myFloat);
                        }
                        break;
                    case "Player Animation":
                        pd.SetGenericBinding(track,attacker.ac.Anim);
                        break;
                    case "Lever Animation":
                        pd.SetGenericBinding(track,victim.ac.Anim);
                        break;
                }
            }
            pd.Evaluate();
            pd.Play();
        }
    }

    private void Update()
    {
        //Debug.Log(1);
//        if (Input.GetKeyDown(KeyCode.H)&&gameObject.layer==LayerMask
//        .NameToLayer("Player"))
//        {
//            pd.Play();
//        }
    }
                
            
//     foreach (PlayableBinding trackBinding in pd.playableAsset.outputs)
//     {
//         switch (trackBinding.streamName)
//         {
//             case "Attacker Script":
//                 pd.SetGenericBinding(trackBinding.sourceObject,attacker);
//                 break;
//             case "Victim Script":
//                 pd.SetGenericBinding(trackBinding.sourceObject,victim);
//                 break;
//             case "Attacker Animation":
//                 pd.SetGenericBinding(trackBinding.sourceObject,attacker
//                     .ac.Anim);
//                 break;
//             case "Victim Animation":
//                 pd.SetGenericBinding(trackBinding.sourceObject,victim.ac
//                     .Anim);
//                 break;
//         }
//     }
}
