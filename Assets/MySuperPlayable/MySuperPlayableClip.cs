using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public MySuperPlayableBehaviour template = new MySuperPlayableBehaviour ();
    public ExposedReference<ActorManager> am;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MySuperPlayableBehaviour>.Create (graph, template);
        MySuperPlayableBehaviour clone = playable.GetBehaviour ();
//        //初始化exposedName的key值
        //am.exposedName=new PropertyName(GetInstanceID().ToString());
       // am.exposedName=System.Guid.NewGuid().ToString(); 
       //Debug.Log(am.exposedName);
        clone.am = am.Resolve (graph.GetResolver ());
        return playable;
    }
}
