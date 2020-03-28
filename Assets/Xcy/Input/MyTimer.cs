using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Xcy.Input
{
    public class MyTimer
    {
        public enum STATE
        {
            Idle,
            Run,
            Finished
        }

        public STATE state;

        public float duration = 1.0f;

        //已流逝时间
        private float _elapsedTime = 0;

        public void Tick()
        {
            switch (state)
            {
                case STATE.Idle:
                    state = STATE.Idle;
                    break;
                case STATE.Run:
                    state = STATE.Run;
                    _elapsedTime += Time.deltaTime;
                    if (_elapsedTime >= duration)
                    {
                        state = STATE.Finished;
                    }

                    break;
                case STATE.Finished:
                    state = STATE.Finished;
                    break;
                default:
                    Debug.LogError("MyTimer Error");
                    break;
            }
        }

        public void Go()
        {
            _elapsedTime = 0;
            state = STATE.Run;
        }

    }
}