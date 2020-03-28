using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;
    public List<EventCasterManager> overlapEcast=new 
    List<EventCasterManager>();
    private void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        EventCasterManager[] ecastms =
            other.GetComponents<EventCasterManager>();
        foreach (EventCasterManager ecastm in ecastms)
        {
            if (!overlapEcast.Contains(ecastm)&&ecastm.active)
            {
                overlapEcast.Add(ecastm);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManager[] ecastms =
            other.GetComponents<EventCasterManager>();
        foreach (EventCasterManager ecastm in ecastms)
        {
            if (overlapEcast.Contains(ecastm))
            {
                overlapEcast.Remove(ecastm);
            }
        }
    }
    
     
    
}
