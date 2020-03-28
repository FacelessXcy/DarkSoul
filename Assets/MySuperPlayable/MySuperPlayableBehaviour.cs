using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float myFloat;
    //private PlayableDirector pd;

    public override void OnGraphStart(Playable playable)
    {
        //pd=(PlayableDirector) playable.GetGraph().GetResolver();
        //Debug.Log("GraphStart");
//        pd = (PlayableDirector)playable.GetGraph().
//            GetResolver();
//        foreach (PlayableBinding track in pd.playableAsset.outputs)
//        {
////            Debug.Log(track.streamName);
//            if (track.streamName=="Attacker Script"||track.streamName=="Victim Script")
//            {
//                
//                ActorManager am = (ActorManager)pd.GetGenericBinding(
//                    track.sourceObject);
//                am.LockUnlockActorController(true);
//            }
//        }
    }

    public override void OnGraphStop(Playable playable)
    {
        
//        if (pd!=null)
//        {
//            pd.playableAsset = null;
//        }
        //Debug.Log("GraphStop");
//        foreach (PlayableBinding track in pd.playableAsset.outputs)
//        {
////            Debug.Log(track.streamName);
//            if (track.streamName=="Attacker Script"||track.streamName=="Victim Script")
//            {
//                
//                ActorManager am = (ActorManager)pd.GetGenericBinding(
//                    track.sourceObject);
//                am.LockUnlockActorController(false);
//            }
//        }
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(true);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
    }
}
