using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class FSMBase
{
    //public T CurrentPlayerState { get; set; }
    //protected List<T> States { get; set; }
    //protected abstract void Initialize();
    //public abstract void SetState(T state);
    //protected abstract void Act();



    //状态机运行时状态参数，当前状态，上一个状态，下一个状态。
    //复杂的状态机状态关联多，需要这三个参数，简单的状态机不需要那么多
    public StateBase CurrentState { get; private set; }
    public StateBase LastState { get; private set; }
    public StateBase NextState { get; private set; }
   
    private List<Enum> history = new List<Enum>();//状态切换历史记录

    //所有状态集合
    private List<(Enum, StateBase)> states = new List<(Enum, StateBase)> (8);
    

    //状态机整体的参数，简单的状态机不需要
    //private StateBase defaultState;//状态机默认状态，也即第一个状态
    private StateBase exitState;//状态机退出状态，也即最后一个状态

    ////状态机回调
    //public delegate void OnStateChange(StateBase currentState, StateBase lastState);//是否带参数/返回值，带什么参数/返回值，根据项目自己确定

    //private OnStateChange OnPreStateChange;//状态改变前
    //private OnStateChange OnPostStateChange;//状态改变后

    //状态机时间
    public float StartTime { get; private set; }
    public float RunTime { get; private set; }

    //状态机主人
    public GameObject FSMMaster { get; private set; }
    /// <summary>
    /// 创建状态机，传入状态机主人
    /// </summary>
    /// <param name="FSMMaster">状态机主人</param>
    public FSMBase(GameObject FSMMaster)//状态机初始化
    {
        this.FSMMaster = FSMMaster;
        //CurrentState = new Player_Idle(this,PlayerState.Player_Idle);
        //CurrentState.EnterState();
        //history.Add(CurrentState.StateId);
        //states.Add(CurrentState.StateId, CurrentState);
        StartTime = Time.time;
        RunTime = 0f;
    }
    /// <summary>
    /// 初始化状态机后，为状态机设置默认状态
    /// </summary>
    /// <param name="defaultState">默认状态</param>
    public void SetDefaultState(StateBase defaultState)
    {
        CurrentState = defaultState;
        CurrentState.EnterState();
        history.Add(CurrentState.StateId);
        states.Add((CurrentState.StateId, CurrentState));
    }
    /// <summary>
    /// 状态机运行
    /// </summary>
    /// <param name="input">玩家输入</param>
    public void UpdateFSM(PlayerInput input)
    {
        if (CurrentState == null)
            return;  
        NextState = CurrentState.InputNextState(input);//处理当前输入，获取下一状态
        if(NextState==null)
        {
            CurrentState.ExitState();
            CurrentState = GetState(PlayerState.Player_Idle);
        }
        if (NextState != CurrentState && NextState != null)
        {
            ChangeState( CurrentState, NextState);
        }
        CurrentState.UpdateState();
        RunTime += Time.deltaTime;
    }
    /// <summary>
    /// 判断States状态集合中是否包含指定的状态
    /// </summary>
    /// <param name="stateId">指定的状态ID</param>
    /// <param name="index">如果包含，该状态在集合中的序列</param>
    /// <returns>返回是否包含指定的状态</returns>
    private bool IsInStates(Enum stateId,out int index)
    {
        for (int i = 0; i < states.Count; ++i)
            if (states[i].Item1.Equals(stateId))
            { 
                index =i; 
                return true; 
            }
        index = -1;
        return false;
    }
    /// <summary>
    /// 在状态集合中添加指定的状态
    /// </summary>
    /// <param name="stateId">指定的状态ID</param>
    /// <param name="state">具体状态</param>
    private void AddState(Enum stateId, StateBase state)
    {
        if(!IsInStates(stateId,out _))
        {
            states.Add((stateId, state));
        }
        //foreach((Enum,StateBase) state1 in states)
        //{
        //    return;
        //}
        //states.Add((stateId, state));
    }

    private void RemoveState(Enum stateId)
    {
        if (IsInStates(stateId,out int index))
        {
            states.RemoveAt(index);
        }
        //for (int i = 0; i < states.Count; ++i)
        //    if (states[i].Item1.Equals(stateId))
        //    {
        //        states.RemoveAt(i);
        //        return;
        //    }
        return ;
    }
    private void ChangeState( StateBase curState,StateBase nextState)
    {
        curState.ExitState();
        //OnPreStateChange?.Invoke(curState, lastState);
        LastState = curState;
        CurrentState = nextState;//状态切换
        //OnPostStateChange?.Invoke(curState, lastState);
        CurrentState.EnterState();
    }
    /// <summary>
    /// 从状态集合中获取指定ID的状态
    /// </summary>
    /// <param name="stateId">指定的状态ID</param>
    /// <returns>获取的状态</returns>
    public StateBase GetState(Enum stateId)
    {
        if(IsInStates(stateId,out int index))
        {
            return states[index].Item2;
        }
        //for (int i = 0; i < states.Count; ++i)
        //    if (states[i].Item1.Equals(stateId))
        //    {
        //        return states[i].Item2;
        //    }
        StateBase state = (StateBase)Activator.CreateInstance(Type.GetType(stateId.ToString()), new object[] { this, stateId });
        states.Add((stateId, state));
        return state;
    }

    public void ForceChangeCurState(StateBase state)//强制转换状态
    {
        ChangeState(CurrentState, state);
    }

    public void ForceStop()//强制状态机退出
    {
        ForceChangeCurState(exitState);
    }

    //public bool isStop => currentState == exitState;//状态机是否停止


    ////状态注册
    //public void RegisterPostStateChange(OnStateChange onPostStateChange)
    //{
    //    OnPostStateChange += onPostStateChange;
    //}

    //public void UnRegisterPostStateChange(OnStateChange onPostStateChange)
    //{
    //    OnPostStateChange -= onPostStateChange;
    //}

    //public void RegisterPreStateChange(OnStateChange onPreStateChange)
    //{
    //    OnPreStateChange += onPreStateChange;
    //}

    //public void UnRegisterPreStateChange(OnStateChange onPreStateChange)
    //{
    //    OnPreStateChange -= onPreStateChange;
    //}

    //public void Reset()//状态机重置
    //{
    //    OnPreStateChange = null;
    //    OnPostStateChange = null;

    //    currentState = defaultState;
    //    lastState = null;
    //    nextState = null;

    //    StartTime = Time.time;
    //    RunTime = 0f;
    //}
    //#endregion

}