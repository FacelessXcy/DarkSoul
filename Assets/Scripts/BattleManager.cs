using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider _defenseCol;
    private void Start()
    {
        _defenseCol = GetComponent<CapsuleCollider>();
        _defenseCol.center = Vector3.up;
        _defenseCol.height = 2f;
        _defenseCol.radius = 0.5f;
        _defenseCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponController targetWc =
            other.GetComponentInParent<WeaponController>();

        if (targetWc==null)
        {
            return;
        }
        
        GameObject attacker = targetWc.wm.am.gameObject;
        //GameObject receiver = am.gameObject;
        GameObject receiver = am.ac.model;        
        
        
        if (other.CompareTag("Weapon"))
        {
//            if (attackingAngle1<=45)
//            {
//                am.TryDoDamage(targetWc);
//            }
            am.TryDoDamage(targetWc,
                CheckAngleTarget(receiver,attacker,45),
            CheckAnglePlayer(receiver,attacker,30));
        }
    }

    //判断玩家与目标是否面对面
    public static bool CheckAnglePlayer(GameObject player,GameObject 
    target,float playerAngleLimit)
    {
        
        Vector3 counterDir = target.transform.position - player
                                 .transform.position;
        //玩家面朝方向与目标方向夹角
        float counterAngle1 = Vector3.Angle(player.transform.forward,
            counterDir);
        //玩家面朝方向与目标面朝方向夹角
        float counterAngle2 = Vector3.Angle(target.transform.forward,
            player.transform.forward);

        //bool attackValid = attackingAngle1 < 45;
        bool counterValid = counterAngle1 < playerAngleLimit &&
                            Mathf.Abs(counterAngle2 - 180) < 
                            playerAngleLimit;
        return counterValid;
    }
    //判断目标是否朝向玩家
    public static bool CheckAngleTarget(GameObject player,GameObject 
    target,float targetAngleLimit)
    {
        
        Vector3 attackingDir = player.transform.position - target
                                   .transform.position;
        float attackingAngle1 = Vector3.Angle(target.transform
            .forward, attackingDir);

        bool attackValid = (attackingAngle1 < targetAngleLimit);
        
        return attackValid;
    }


}
