using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    
    [Header("== Auto Generate if Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    public EventCasterManager ecm;

    public ActorManager(ActorController ac, BattleManager bm, WeaponManager wm, StateManager sm)
    {
        this.ac = ac;
        this.bm = bm;
        this.wm = wm;
        this.sm = sm;
    }

    private void Awake()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        //Debug.Log(model==null);
        GameObject sensor = transform.Find("sensor").gameObject;
        
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
        //ecm = Bind<EventCasterManager>(caster);
        //sm.Test();
        ac.OnAction += DoAction;
    }

    public void DoAction()
    {
        if (im.overlapEcast.Count!=0)
        {
            if (im.overlapEcast[0].active)
            {
                //Play corresponding timeline here
                if (im.overlapEcast[0].eventName=="frontStab")
                {
                    dm.PlayFrontStab("frontStab",this,
                        im.overlapEcast[0].am);
                }else if (im.overlapEcast[0].eventName=="openBox")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model,im
                    .overlapEcast[0].am.gameObject,180))
                    {
                        im.overlapEcast[0].active = false;
                        transform.position = im.overlapEcast[0].am
                                                 .transform.position +
                                             im.overlapEcast[0].am
                                             .transform
                                             .TransformVector(im
                                             .overlapEcast[0].offset);
                        ac.model.transform.LookAt(im.overlapEcast[0]
                        .am.transform,Vector3.up);
                        dm.PlayFrontStab("openBox",this,
                            im.overlapEcast[0].am);
                    }
                }else if (im.overlapEcast[0].eventName=="leverUp")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model,im
                        .overlapEcast[0].am.gameObject,180))
                    {
                        //im.overlapEcast[0].active = false;
                        transform.position = im.overlapEcast[0].am
                                                 .transform.position +
                                             im.overlapEcast[0].am
                                                 .transform
                                                 .TransformVector(im
                                                     .overlapEcast[0].offset);
                        ac.model.transform.LookAt(im.overlapEcast[0]
                            .am.transform,Vector3.up);
                        dm.PlayFrontStab("leverUp",this,
                            im.overlapEcast[0].am);
                    }
                }
            }
        }
    }

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnabled = value;
    }

    /// <summary>
    /// 双向绑定,该组件以及指定组件
    /// </summary>
    /// <param name="go">需要绑定的组件所在的gameObject</param>
    /// <typeparam name="T">需要绑定的组件</typeparam>
    /// <returns></returns>
    private T Bind<T>(GameObject go) where T:IActorManagerInterface
    {
        T tempInstance = null;
        tempInstance = go.GetComponent<T>();
        if (tempInstance==null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    public void TryDoDamage(WeaponController targetWc,bool 
    attackValid,bool counterValid)
    {
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned(); 
            }
        }
        else if (sm.iscounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(false);
            }
        }
        else if (sm.isImmortal)
        {
            //Do nothing
        }
        else if (sm.isDefense)
        {
            //attack should be blocked
            Blocked(); 
        }else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }
        }
        
    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void HitOrDie(bool doHitAnimation=true)
    {
        if (sm.hp<=0)
        {
            //dead 
        }
        else
        {
            sm.AddHp(-5);
            if (sm.hp>0)
            {
                if (doHitAnimation)
                {
                    Hit(); 
                }
                //do some VFX,like splatter blood
            }
            else
            {
                Die();
            }
        }   
    }

    public void Hit()
    { 
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.playerInput.inputEnabled = false;
        if (ac.cameraController.lockState)
        {
            ac.cameraController.LockUnlock();
        }
        ac.cameraController.enabled = false;
    }

    public void TestEcho()
    {
        //Debug.Log("Echo");
    }

    public void LockUnlockActorController(bool value)
    {
        if (value)
        {
            ac.SetBool("lock",true);
            if (wm!=null)
            {
                wm.WeaponDisable();
            }
        }
        else
        {
            ac.SetBool("lock",false);
            //wm.WeaponEnable();
        }
        
        //Debug.Log("LockAC  "+value);
    }
    

}
