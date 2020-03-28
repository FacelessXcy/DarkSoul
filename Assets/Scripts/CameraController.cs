using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public bool lockState = false;
    public Image lockDot; 
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private IUserInput m_playerInput;
    public float horizontalSpeed = 20.0f;
    public float VerticalSpeed = 20.0f;

    public GameObject model;
    private float tempEulerX=20.0f;
    private Vector3 cameraSmoothDampVelo;
    private Camera _camera;
    [SerializeField]
    private LockTarget _lockTarget;

    public bool isAI = false;
    
    void Awake () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        m_playerInput = playerHandle.GetComponent<IUserInput>();
        //model = playerHandle.transform.Find("chara180530_2_normalEdited").gameObject;

        if (!isAI)
        {
            _camera = Camera.main;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        lockState = false;
        
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (_lockTarget!=null)
        {
            if (!isAI)
            {
                lockDot.rectTransform.position = _camera.WorldToScreenPoint
                (_lockTarget.obj.transform.position + Vector3
                    .up * _lockTarget.halfHeight);
            }
            if (_lockTarget.am!=null&&_lockTarget.am.sm.isDie)
            {
                LockProcessA(null, false, 
                    false, isAI);
            }
            if (Vector3.Distance(model.transform.position,
                _lockTarget.obj.transform.position)>10.0F)
            {
                LockProcessA(null, false, 
                    false, isAI);
            }
            
            
        }
    }

    void FixedUpdate() {
        if (_lockTarget==null)
        {
            //Debug.Log(model==null);
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up,
                m_playerInput.Jright * horizontalSpeed 
                                     * Time.fixedDeltaTime);
            //cameraHandle.transform.Rotate(Vector3.right, -m_playerInput.Jup * VerticalSpeed
            //* Time.deltaTime);无法跨过0度角
            //tempEulerX =cameraHandle.transform.eulerAngles.x;
            tempEulerX -= m_playerInput.Jup * VerticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);

            
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX,0,0);
            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = _lockTarget.obj.transform.position - 
            model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(_lockTarget.obj.transform);
        }

        if (_lockTarget!=null)
        {
            lockDot.transform.position = _camera.WorldToScreenPoint
                    (_lockTarget.obj.transform.position);
        }

        if (!isAI)
        {
            //camera.transform.position = Vector3.Lerp(camera.transform.position,transform.position,0.4f);
            _camera.transform.position = Vector3.SmoothDamp(
                _camera.transform.position, transform.position, 
                ref cameraSmoothDampVelo, 0.05f);
            //_camera.transform.rotation = transform.rotation;
            _camera.transform.LookAt(cameraHandle.transform);
        }
    }
    //镜头索敌
    public void LockUnlock()
    {
        //Debug.Log("lockunlock");
        //Try to lock.
            Vector3 modelOrigin1 = playerHandle.transform.position;
            Vector3 modelOrigin2 = modelOrigin1 + Vector3.up;
            Vector3 boxCenter =
                modelOrigin2 + playerHandle.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3
                (0.5f, 0.5f, 5f), playerHandle.transform.rotation,LayerMask
                .GetMask(isAI?"Player":"Enemy"));
            if (cols.Length==0)
            {
                LockProcessA(null,false,
                    false,isAI);
            } 
            else
            {
                foreach (Collider col in cols)
                {
                    if (_lockTarget!=null&&_lockTarget.obj==col
                    .gameObject)
                    {
                        LockProcessA(null,false,
                            false,isAI);
                        break;
                    }
//                    _lockTarget = new LockTarget(col.gameObject,
//                        col.bounds.extents.y);//问题！！！col.bounds.extends
//                    lockDot.enabled = true;
//                    lockState = true;
                    LockProcessA(new LockTarget(col.gameObject,
                            col.bounds.extents.y),true,
                        true,isAI);
                    break;
                }
            }

    }
    
    private class LockTarget
    {
        public GameObject obj;
        public ActorManager am;
        public float halfHeight;

        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = obj.GetComponent<ActorManager>();
        }
    }

    private void LockProcessA(LockTarget lockTarget,bool 
    lockDotEnable,bool lockState,bool isAI)
    {
        _lockTarget = lockTarget;
        if (!isAI)
        {
            lockDot.enabled = lockDotEnable;
        }
        this.lockState = lockState;
    }
}
