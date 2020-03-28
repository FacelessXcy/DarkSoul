using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.FSM;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FSMSystem
{
	private List<FSMState> _states=new List<FSMState>();
	private FSMState _currentState;
	public FSMState CurrentState => _currentState;

	public void AddStates(params FSMState[] states)
	{
		foreach (FSMState state in states)
		{
			AddState(state);
		}
	}

	public void AddState(FSMState state)
	{
		if (_states==null)
		{
			_states=new List<FSMState>();
		}
		if (state==null)
		{
			Debug.LogError("要添加的状态为空");
			return;
		}
		if (_states.Count==0)
		{
			_states.Add(state);
			_currentState = state;
			_currentState.DoBeforeEntering();
			return;
		}
		foreach (FSMState fsmState in _states)
		{
			if (fsmState.StateID==state.StateID)
			{
				Debug.Log("要添加的状态ID"+state.StateID+"已添加");
				return;
			}
			_states.Add(state);
		}
	}
	public void DeleteState(StateID stateID)
	{
		if (stateID==StateID.NullState)
		{
			Debug.LogError("要删除的状态ID["+stateID+"]为空状态");
			return;
		}
		foreach (FSMState state in _states)
		{
			if (state.StateID==stateID)
			{
				_states.Remove(state);
				return;
			}
		}
		Debug.LogError("要删除的状态ID["+stateID+"]不存在");
	}
	/// <summary>
	/// 执行转换
	/// </summary>
	/// <param name="trans">转换条件</param>
	public void PerformTransition(Transition trans)
	{
		if (trans==Transition.NullTransition)
		{
			Debug.LogError("敌人状态 要执行的转换条件为空"+trans);
			return;
		}

		StateID nextStateID = _currentState.GetOutputState(trans);
		if (nextStateID==StateID.NullState)
		{
			Debug.LogError("敌人状态 要执行的转换状态为空"+nextStateID);
			return;
		}
		foreach (FSMState fsmState in _states)
		{
			if (fsmState.StateID==nextStateID)
			{
				_currentState.DoBeforeLeaving();
				_currentState = fsmState;
				_currentState.DoBeforeEntering();
				return;
			}
		}
	}


}
