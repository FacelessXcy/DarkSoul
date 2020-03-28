using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StateManager : IActorManagerInterface
{
    //public ActorManager am;

    public float hp = 15;
    public float hpMax = 15;

    [Header("=== 1st order state flag ===")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack=false;//是否在状态机内处于CounterBack状态
    public bool isCounterBackEnabled;//与Animation Event相关

    [Header("=== 2en order state flag ===")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool iscounterBackFailure;
    
    private void Start()
    {
        hp = hpMax;
    }


    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR")||am.ac.CheckStateTag("attackL");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defense1h","defense");
        //CheckState("defense1h","defense")
        isCounterBack = am.ac.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnabled;
        iscounterBackFailure = isCounterBack && !isCounterBackEnabled;

        isAllowDefense = isGround || isBlocked;
        isDefense=isAllowDefense&&am.ac.CheckState("defense1h","defense");
        isImmortal = isRoll || isJab; 
        
    }

    public void Test()
    {
        //Debug.Log("test");
    }

    

    public void AddHp(float value)
    {
        hp += value;
        hp = Mathf.Clamp(hp, 0, hpMax);
//        if (hp>0)
//        {
//            am.Hit();
//        }
//        else
//        {
//            am.Die();
//        }
        
    }
}
