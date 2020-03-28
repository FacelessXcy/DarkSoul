using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator _anim;
    private ActorController ac;
    public Vector3 a;
    public Transform te;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        ac = GetComponentInParent<ActorController>();
    }
 
    private void OnAnimatorIK(int layerIndex)
    {
        if (_anim.GetBool("defense"))
        {
            return;
        }
        if (ac.leftIsShield)
        {
            //Transform leftLowerArm1 = _anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            Transform leftLowerArm = _anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            //Debug.Log(System.Object.ReferenceEquals((object)leftLowerArm,
            // (object)leftLowerArm1));
            leftLowerArm.localEulerAngles += 0.75f*a;
            _anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm,
                Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
        
        
    }
}
