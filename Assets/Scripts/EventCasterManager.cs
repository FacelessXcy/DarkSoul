using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EventCasterManager : IActorManagerInterface
{

    public string eventName;
    public bool active;
    public Vector3 offset=new Vector3(0,0,0.5f);

    private void Start()
    {
        if (am==null)
        {
            am = GetComponentInParent<ActorManager>();
        }
    }
}
