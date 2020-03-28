using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public TestPlayableBehaviour template = new TestPlayableBehaviour ();
    public ExposedReference<ActorController> newExposedReference;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TestPlayableBehaviour>.Create (graph, template);
        TestPlayableBehaviour clone = playable.GetBehaviour ();
        clone.newExposedReference = newExposedReference.Resolve (graph.GetResolver ());
        return playable;
    }
}
