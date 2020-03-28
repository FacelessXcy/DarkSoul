using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput {
    [Header("===== Key Settings=====")]
    public string keyUp="w";
    public string keyDown="s";
    public string keyRight="d";
    public string keyLeft="a";

    public string keyA="left shift";
    public string keyB="space";
    public KeyCode keyC=KeyCode.CapsLock;
    public string keyD="f";
    public string keyE;
    public string keyF;
    public string keyLB;
    public KeyCode keyLT;
    public string keyRB;
    public KeyCode keyRT;
    
    public string keyJUp;
    public string keyJDown;
    public string keyJRight;
    public string keyJLeft;
    
    
    public MyButton buttonA=new MyButton();
    public MyButton buttonB=new MyButton();
    public MyButton buttonC=new MyButton();
    public MyButton buttonD=new MyButton();
    public MyButton buttonE=new MyButton();
    public MyButton buttonF=new MyButton();
    public MyButton buttonLB=new MyButton();
    public MyButton buttonLT=new MyButton();
    public MyButton buttonRB=new MyButton();
    public MyButton buttonRT=new MyButton();
    
    [Header("===== Mouse Settings=====")]
    
    public bool mouseEnable=false;

    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

//    [Header("===== Output Signals=====")]
//    public float Dup; 
//    public float Dright;
//    public float Dmag;
//    public Vector3 Dvec;
//
//    public float Jup;
//    public float Jright;
//    
//    
//    
//    //1.按压型信号
//    public bool run;
//    //2一次触发信号
//    public bool jump;
//    private bool lastJump;
//    public bool attack;
//    private bool lastAttack; 
//    //3双击型信号
//
//
//    [Header("===== Others=====")]
//    private float targetDup;
//    private float targetDright;
//    private float velocityDup;
//    private float velocityDright;
//    public bool inputEnabled = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(keyC));
        buttonD.Tick(Input.GetKey(keyD));
//        buttonE.Tick(Input.GetKey(keyE));  
        buttonF.Tick(Input.GetKey(keyF));
        //Debug.Log(buttonA.isExtending&&buttonA.onPressed);
        //Debug.Log(buttonC.isPressing);
        buttonLB.Tick(Input.GetKey(keyLB));
        buttonLT.Tick(Input.GetKey(keyLT));
        buttonRB.Tick(Input.GetKey(keyRB));
        buttonRT.Tick(Input.GetKey(keyRT));
        
        //镜头移动
        if (mouseEnable)
        {
            Jup = Input.GetAxis("Mouse Y")*mouseSensitivityY*2.0f;
            Jright = Input.GetAxis("Mouse X")*mouseSensitivityX*3.0f;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }
        //角色移动
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled==false)
        {
            targetDup = 0;
            targetDright=0;

        }

        Dup = Mathf.SmoothDamp(Dup,targetDup,ref velocityDup,0.1f);
        //Debug.Log(velocityDup+ "velocityDup");
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright,0.1f);
        //Debug.Log(velocityDright + "velocityDright");

        Vector2 tempDAxis=SquareToCircle(new Vector2(Dright,Dup));//转换坐标后的坐标值
        
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;
        
//        Dmag = Mathf.Sqrt(Dup2*Dup2 + Dright2*Dright2);
//        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
        UpdateDmagMvec(Dup2,Dright2);

        
        run = (buttonA.isPressing&&!buttonA.isDelaying)||buttonA.isExtending ;
        jump = buttonA.onPressed&&buttonA.isExtending;
        roll = buttonA.onReleased && buttonA.isDelaying;
        action = buttonD.onPressed;
 
        defense = buttonF.isPressing;
        //Debug.Log(defense+" KB"+gameObject.name);
        //attack = buttonE.onPressed;
        rb = buttonRB.onPressed;
        rt = buttonRT.onPressed;
        lb = buttonLB.onPressed;
        lt = buttonLT.onPressed;
        lockon = buttonC.onPressed;
    }

    

}
