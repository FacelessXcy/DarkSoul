using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DummyIUserInput : IUserInput
{
    IEnumerator Start()
    {
        while (true)
        {
//            Dup = 1.0f;
//            Dright = 0;
//            Jright = 1;
//            Jup = 0;
//            run = true;
//            yield return new WaitForSeconds(3.0f);
//            Dup = 0f;
//            Dright = 0;
//            Jright = 0;
//            Jup = 0;
//            run = true;
//            yield return new WaitForSeconds(1.0f);
            rb = true;
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void Update()
    {
        UpdateDmagMvec(Dup, Dright);
    }
}
