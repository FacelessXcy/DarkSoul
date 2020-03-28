using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Xcy
{
	public class MsgDispatcher
	{
		///全局记录
		private static Dictionary<string, Action<object>> _registeredMsgsWith1Para = new Dictionary<string, Action<object>>();
		private static Dictionary<string,Action> _registeredMsgsWith0Para=new Dictionary<string, Action>();

		#region ZeroParameter
		public static void Register(string msgName, Action onMsgReceived)
		{
			if (!_registeredMsgsWith0Para.ContainsKey(msgName))
			{
				_registeredMsgsWith0Para.Add(msgName, () => { });
			}

			_registeredMsgsWith0Para[msgName] += onMsgReceived;
		}

		public static void UnRegister(string msgName, Action onMsgReceived)
		{
			if (_registeredMsgsWith0Para.ContainsKey(msgName))
			{
				_registeredMsgsWith0Para[msgName] -= onMsgReceived;
			}
		}

		public static void Send(string msgName)
		{
			if (_registeredMsgsWith0Para.ContainsKey(msgName))
			{
				_registeredMsgsWith0Para[msgName]();
			}
		}
		

		#endregion
		
		#region OneParameter
		
		public static void Register(string msgName, Action<object> onMsgReceived)
		{
			if (!_registeredMsgsWith1Para.ContainsKey(msgName))
			{
				_registeredMsgsWith1Para.Add(msgName, _ => { });
			}

			_registeredMsgsWith1Para[msgName] += onMsgReceived;
		}

		public static void UnRegister(string msgName, Action<object> onMsgReceived)
		{
			if (_registeredMsgsWith1Para.ContainsKey(msgName))
			{
				_registeredMsgsWith1Para[msgName] -= onMsgReceived;
			}
		}

		public static void Send(string msgName, object data)
		{
			if (_registeredMsgsWith1Para.ContainsKey(msgName))
			{
				_registeredMsgsWith1Para[msgName](data);
			}
		}
		#endregion
		public static void UnRegisterAll(string msgName,int paraCount)
		{
			switch (paraCount)
			{
				case 0:
					_registeredMsgsWith0Para.Remove(msgName);
					break;
				case 1:
					_registeredMsgsWith1Para.Remove(msgName);
					break;
				default:
					Debug.LogError("无当前参数数目的Action回调函数:{"+paraCount+"},无法删除");
					break;
			}
		}
	}
}
