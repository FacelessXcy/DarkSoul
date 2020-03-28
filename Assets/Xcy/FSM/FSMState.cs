using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcy.FSM
{
	public enum StateID
	{
		NullState = 0
	}

	public enum Transition
	{
		NullTransition = 0
	}

	public abstract class FSMState
	{
		protected FSMSystem _Fsm;

		protected FSMState(FSMSystem fsm)
		{
			_Fsm = fsm;
		}

		protected Dictionary<Transition,StateID> _Map=new Dictionary<Transition, StateID>();
		protected StateID _stateID;
		public StateID StateID => _stateID;

		public void AddTransition(Transition trans,StateID stateID)
		{
			if (trans==Transition.NullTransition)
			{
				Debug.LogError("添加转换条件失败，不可为空条件");
				return;
			}
			if (stateID==StateID.NullState)
			{
				Debug.LogError("添加转换状态失败，不可为空状态");
				return;
			}
			if (_Map.ContainsKey(trans))
			{
				Debug.LogError("添加转换条件失败，以添加过条件");
				return;
			}
			_Map.Add(trans,stateID);
		}
		public void DeleteTransition(Transition trans)
		{
			if (!_Map.ContainsKey(trans))
			{
				Debug.LogError("删除转换条件失败，无"+trans+"条件");
			}
			_Map.Remove(trans);
		}
		/// <summary>
		/// 获取该转换条件发生时，要转入的状态
		/// </summary>
		/// <param name="trans"></param>
		/// <returns></returns>
		public StateID GetOutputState(Transition trans)
		{
			if (!_Map.ContainsKey(trans))
			{
				Debug.LogError("敌人状态 获取转换状态失败，无"+trans+"条件");
				return StateID.NullState;
			}
			return _Map[trans];
		}

		public abstract void DoBeforeEntering();


		public abstract void DoBeforeLeaving();
		

		public abstract void Reason(Transform player);//转换条件判断
		public abstract void Act(Transform player);//状态活动

	}
}