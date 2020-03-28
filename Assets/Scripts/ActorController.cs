using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ActorController : MonoBehaviour {
    #region Attribute

    public CameraController cameraController;
    public GameObject model;
    public IUserInput playerInput;
    public float runMultiplier;
    public float walkSpeed;
    public float jumpSpeed=4.0f;
    public float rollSpeed=1.0f;
    public float jabMultiplier = 3.0f;
    
    [Space(10)]
    [Header("===== Friction Settings =====")]
    [SerializeField]private PhysicMaterial frictionOne;
    [SerializeField]private PhysicMaterial frictionZero;
    
    private Animator anim;

    public Animator Anim => anim;

    private bool canAttack;

    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;
    //平面移动锁定
    private bool lockPlanar = false;
    private bool trackDirection = false;

    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec; //跳跃冲量向量
    
    [FormerlySerializedAs("_leftIsShield")] [SerializeField]
    public bool leftIsShield=true;

    public int count = 0;

    public delegate void OnActionDelegate();

    public event OnActionDelegate OnAction;
    
#endregion

    void Awake () {
        //model = transform.Find("chara180530_2_normalEdited").gameObject;
        anim = model.GetComponent<Animator>();
        if (playerInput==null)
        {
            playerInput = gameObject.GetComponent<IUserInput>();
        }
        rigid =  gameObject.GetComponent<Rigidbody>();
        frictionOne = Resources.Load<PhysicMaterial>("frictionOne");
        frictionZero = Resources.Load<PhysicMaterial>("frictionZero");
        col = GetComponent<CapsuleCollider>();
    }

    #region UpdateCode
    void Update () {
        //Debug.Log(m_PlayerInput.Dup);
        //Debug.Log(playerInput.lockon);
        if (playerInput.lockon)
        {
            cameraController.LockUnlock();
        }

        if (!cameraController.lockState)
        {
            anim.SetFloat("forward",
                playerInput.Dmag * Mathf.Lerp(
                    anim.GetFloat("forward"),
                    playerInput.run ? 2.0f : 1.0f, 0.2f));
            anim.SetFloat("right",0);
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformVector
                (playerInput.Dvec);
            anim.SetFloat("forward",
                localDvec.z * (playerInput.run ? 2.0f : 1.0f));
            anim.SetFloat("right",
                localDvec.x * (playerInput.run ? 2.0f : 1.0f));
            
        }

        //anim.SetBool("defense",playerInput.defense);
        
        if (playerInput.roll||rigid.velocity.magnitude>7)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (playerInput.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if ((playerInput.rb||playerInput.lb)&&(CheckState("ground")
        ||CheckStateTag("attackR")||CheckStateTag("attackL"))&&canAttack)
        {
            if (playerInput.rb)
            {
                anim.SetBool("R0L1",false);
                anim.SetTrigger("attack");
            }
            else if(playerInput.lb&&!leftIsShield)
            {
                anim.SetBool("R0L1",true);
                anim.SetTrigger("attack");
            }
        }

        if ((playerInput.rt||playerInput.lt)&&(CheckState("ground")
        ||CheckStateTag("attackR")||CheckStateTag("attackR"))&&canAttack)
        {
            if (playerInput.rt)
            {
                //do right heavy attack
            }
            else
            {
                if (!leftIsShield)
                {
                    //do left heavy attack
                }
                else
                {
                    anim.SetTrigger("counterBack");
                }
            }
        }

        
        
        if (leftIsShield)
        {
            if (CheckState("ground")||CheckState("blocked"))
            {
                anim.SetBool("defense",playerInput.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"),1);
            }
            else
            {
                anim.SetBool("defense",false);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"),0);
            }
            
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"),0);
        }
        //anim.SetLayerWeight(anim.GetLayerIndex("defense"),0);
        

        if (!cameraController.lockState)
        {
            if (playerInput.inputEnabled)
            {
                
                if (playerInput.Dmag>0.01f)
                {
                    //Vector3 targetForward = Vector3.Slerp(model.transform.forward, m_PlayerInput.Dvec,0.3f);
                    model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dvec, 0.3f);
                }
           
            }
            if (lockPlanar==false)
            {
                planarVec = playerInput.Dmag * model.transform.forward *
                            (playerInput.run ? runMultiplier : 1.0f);
            }

            //Debug.Log(planarVec);
        }
        else
        {
            if (!trackDirection)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }
            
            if (!lockPlanar)
            {
                planarVec = playerInput.Dvec *
                            (playerInput.run ? runMultiplier : 1.0f);
            }
           
        }
        if (playerInput.action)
        {
            OnAction.Invoke();
        }
        
        
    }
    

    private void FixedUpdate()
    {
        //rigid.position += planarVec * Time.fixedDeltaTime*walkSpeed;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x * walkSpeed, 
                             rigid.velocity.y, planarVec.z * walkSpeed) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;


        //Debug.Log(CheckState("idle","attack"));
    }
    #endregion

    public bool CheckState(string stateName, string layerName="Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(
            anim.GetLayerIndex(layerName)).IsName(stateName);
    } 

    public bool CheckStateTag(string tagName,string layerName="Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(
            anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    #region  FSMEvent
    /// <summary>
    /// Messaage processing block
    /// </summary>
    public void OnJumpEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0,jumpSpeed,0);
        trackDirection = true;
        //Debug.Log("OnJumpEnte");
    }
    private void OnRollEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollSpeed, 0);
        trackDirection = true;
    }

    private void OnJabEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        
    }

    private void OnJabUpdate()
    {
        thrustVec = model.transform.forward * jabMultiplier*(anim.GetFloat("jabVelocity"));
        //Debug.Log(anim.GetFloat("jabVelocity"));
    }

    //public void OnJumpExit()
   // {
        //m_PlayerInput.inputEnabled = true;
        //lockPlanar = false;
        //Debug.Log("OnJumpExit");
    //}
    private  void IsGround()
    {
        //Debug.Log("On ground");
        anim.SetBool("isGround",true);
    }
    private void IsNotGround()
    {
        //Debug.Log("Not On Ground");
        anim.SetBool("isGround", false);
    }

    private void OnGroundEnter()
    {
        playerInput.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    private void OnGroundExit()
    {
        col.material = frictionZero;
    }

    private void OnFallEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

//    public void OnAttackIdleEnter()
//    {
//        playerInput.inputEnabled = true;
//        //lockPlanar = false;
//        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0f);
//        //lerpTarget = 0;
//    }

//    public void OnAttackIdleUpdate()
//    {
////        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp
////        (anim.GetLayerWeight(anim.GetLayerIndex("attack")),
////            lerpTarget, 0.4f));
//    }

    private void OnAttack1hAEnter()
    {
        playerInput.inputEnabled = false;
        //lockPlanar = true;
        //lerpTarget = 1.0f;
    }

    private void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward* anim.GetFloat(
        "attack1hAVelocity");
        //Debug.Log(anim.GetFloat("attack1hAVelocity"));
//        anim.SetLayerWeight(anim.GetLayerIndex("attack"),Mathf.Lerp(
//            anim.GetLayerWeight(anim.GetLayerIndex("attack")),
//            lerpTarget, 0.4f));
    }

    public void OnAttackExit()
    {
        //Debug.Log("exit");
        model.SendMessage("WeaponDisable");
    }

    public void OnUpdateRM(object _deltaPos)
    {
//        Debug.Log(_deltaPos);
        if (CheckState("attack1hC"))
        {
            //Debug.Log(((Vector3)_deltaPos).x);
            deltaPos += (Vector3)_deltaPos*0.6f+0.4f*deltaPos;
        }
    }

    public void OnHitEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnBlockEnter()
    {
        playerInput.inputEnabled = false;
    }

    public void OnDieEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnStunnedEnter()
    {
        playerInput.inputEnabled = false;
        planarVec=Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        playerInput.inputEnabled = false;
        planarVec=Vector3.zero;
    }

    public void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }

    public void OnLockEnter()
    {
        playerInput.inputEnabled = false;
        planarVec=Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    #endregion

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string boolName,bool value)
    {
        anim.SetBool(boolName,value);
    }
    

}
