using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class IUserInput : MonoBehaviour
{
    [Header("===== Output Signals=====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public float Jup;
    public float Jright;
    
    
    
    //1.按压型信号
    public bool run;
    public bool defense;
    //2一次触发信号
    public bool action;
    public bool jump;
    protected bool lastJump;
    //public bool attack;
    protected bool lastAttack;
    public bool roll;
    public bool lockon;
    public bool lb;
    public bool lt;
    public bool rb;
    public bool rt;
    
    //3双击型信号

    [Header("===== Others=====")]
    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;
    public bool inputEnabled = true;
    
    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected void UpdateDmagMvec(float Dup2, float Dright2)
    {
        Dmag = Mathf.Sqrt(Dup2*Dup2 + Dright2*Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }
}
