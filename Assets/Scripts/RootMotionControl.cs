using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RootMotionControl : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRM",
            animator.deltaPosition);
    }
}
