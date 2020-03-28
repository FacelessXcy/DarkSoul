using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.4433962f, 0.8867924f)]
[TrackClipType(typeof(TestPlayableClip))]
[TrackBindingType(typeof(ActorController))]
public class TestPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TestPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
