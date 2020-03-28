using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableBehaviour : PlayableBehaviour
{
    public ActorController newExposedReference;
    public ActorController newBehaviourVariable;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
