using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Xcy.Utility;

namespace Xcy
{
	public abstract class XMonoBehaviour : MonoBehaviour
	{
        #region Delay
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
        #endregion

        #region MsgDispatcher
        //该脚本内的记录
        private List<MsgRecord> _msgRecorder = new List<MsgRecord>();

        class MsgRecord
        {
            public string Name;
            public Action<object> OnMsgReceived;
            private MsgRecord() { }

            private static Stack<MsgRecord> _msgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
            {
                var retRecord = _msgRecordPool.Count > 0 ? _msgRecordPool.Pop() : new MsgRecord();
                retRecord.Name = msgName;
                retRecord.OnMsgReceived = onMsgReceived;

                return retRecord;
            }

            public void Recycle()
            {
                Name = null;

                OnMsgReceived = null;

                _msgRecordPool.Push(this);
            }
        }

        public void RegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            MsgDispatcher.Register(msgName, onMsgReceived);
            _msgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }

        public void SendMsg(string msgName, object data)
        {
            MsgDispatcher.Send(msgName, data);
        }
        

        public void UnRegisterMsg(string msgName)
        {
            var selectedRecords = _msgRecorder.FindAll(record => record.Name == msgName);

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                _msgRecorder.Remove(record);

                record.Recycle();
            });


            selectedRecords.Clear();
        }

        public void UnRegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            var selectedRecords = _msgRecorder.FindAll(
                record => record.Name == msgName && record.OnMsgReceived == onMsgReceived
                          );

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                _msgRecorder.Remove(record);

                record.Recycle();
            });


            selectedRecords.Clear();
        }

        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgRecord in _msgRecorder)
            {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            _msgRecorder.Clear();
        }

        protected abstract void OnBeforeDestroy();

        #endregion

	}
}