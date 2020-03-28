using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WeaponManager : IActorManagerInterface
{
    private Collider weaponColL;
    private Collider weaponColR;
    
    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;
    
    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);

        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();
//        weaponCol = whR.GetComponentInChildren<Collider>();
//        Debug.Log(transform.DeepFind("weaponHandleR"));
//        Debug.Log(transform.DeepFind("weaponHandleL"));
        WeaponDisable();
    }


    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if (tempWc==null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }

        tempWc.wm = this;
        return tempWc;
    }

    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
        }
        else
        {
            weaponColR.enabled = true;
        }
//        Debug.Log("WeaponEnable"); 
    }
    
    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
        //Debug.Log("WeaponDisable");
    }

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }
    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }

}
