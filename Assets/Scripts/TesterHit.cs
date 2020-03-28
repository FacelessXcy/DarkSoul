using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TesterHit : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("hit");
        }
    }
}
