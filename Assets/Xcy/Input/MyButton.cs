using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Xcy.Input
{
    public class MyButton
    {
        public bool isPressing = false;
        public bool onPressed = false;
        public bool onReleased = false;
        public bool isExtending = false;
        public bool isDelaying = false;

        //用于DoubleTrigger
        public float extendingDuration = 0.15f;

        //用于LongPress
        public float delayingDuration = 0.15f;

        private bool _curState = false;
        private bool _lastState = false;
        private MyTimer _extTimer = new MyTimer();
        private MyTimer _delayTimer = new MyTimer();

        public void Tick(bool input)
        {
            //StartTimer(_extTimer,1.0f);
            _extTimer.Tick();
            _delayTimer.Tick();
            //Debug.Log(_extTimer.state);
            _curState = input;
            isPressing = _curState;

            onPressed = false;
            onReleased = false;
            isExtending = false;
            isDelaying = false;

            if (_curState != _lastState)
            {
                if (_curState)
                {
                    onPressed = true;
                    StartTimer(_delayTimer, delayingDuration);
                }
                else
                {
                    onReleased = true;
                    //开始延长isExtending时间
                    StartTimer(_extTimer, extendingDuration);
                }

            }

            _lastState = _curState;

            if (_extTimer.state == MyTimer.STATE.Run)
            {
                isExtending = true;
            }

            if (_delayTimer.state == MyTimer.STATE.Run)
            {
                isDelaying = true;
            }

        }

        private void StartTimer(MyTimer timer, float duration)
        {
            timer.duration = duration;
            timer.Go();
        }

    }
}