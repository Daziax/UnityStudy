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



    //״̬������ʱ״̬��������ǰ״̬����һ��״̬����һ��״̬��
    //���ӵ�״̬��״̬�����࣬��Ҫ�������������򵥵�״̬������Ҫ��ô��
    public StateBase CurrentState { get; private set; }
    public StateBase LastState { get; private set; }
    public StateBase NextState { get; private set; }
   
    private List<Enum> history = new List<Enum>();//״̬�л���ʷ��¼

    //����״̬����
    private List<(Enum, StateBase)> states = new List<(Enum, StateBase)> (8);
    

    //״̬������Ĳ������򵥵�״̬������Ҫ
    //private StateBase defaultState;//״̬��Ĭ��״̬��Ҳ����һ��״̬
    private StateBase exitState;//״̬���˳�״̬��Ҳ�����һ��״̬

    ////״̬���ص�
    //public delegate void OnStateChange(StateBase currentState, StateBase lastState);//�Ƿ������/����ֵ����ʲô����/����ֵ��������Ŀ�Լ�ȷ��

    //private OnStateChange OnPreStateChange;//״̬�ı�ǰ
    //private OnStateChange OnPostStateChange;//״̬�ı��

    //״̬��ʱ��
    public float StartTime { get; private set; }
    public float RunTime { get; private set; }

    //״̬������
    public GameObject FSMMaster { get; private set; }
    /// <summary>
    /// ����״̬��������״̬������
    /// </summary>
    /// <param name="FSMMaster">״̬������</param>
    public FSMBase(GameObject FSMMaster)//״̬����ʼ��
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
    /// ��ʼ��״̬����Ϊ״̬������Ĭ��״̬
    /// </summary>
    /// <param name="defaultState">Ĭ��״̬</param>
    public void SetDefaultState(StateBase defaultState)
    {
        CurrentState = defaultState;
        CurrentState.EnterState();
        history.Add(CurrentState.StateId);
        states.Add((CurrentState.StateId, CurrentState));
    }
    /// <summary>
    /// ״̬������
    /// </summary>
    /// <param name="input">�������</param>
    public void UpdateFSM(PlayerInput input)
    {
        if (CurrentState == null)
            return;  
        NextState = CurrentState.InputNextState(input);//����ǰ���룬��ȡ��һ״̬
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
    /// �ж�States״̬�������Ƿ����ָ����״̬
    /// </summary>
    /// <param name="stateId">ָ����״̬ID</param>
    /// <param name="index">�����������״̬�ڼ����е�����</param>
    /// <returns>�����Ƿ����ָ����״̬</returns>
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
    /// ��״̬���������ָ����״̬
    /// </summary>
    /// <param name="stateId">ָ����״̬ID</param>
    /// <param name="state">����״̬</param>
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
        CurrentState = nextState;//״̬�л�
        //OnPostStateChange?.Invoke(curState, lastState);
        CurrentState.EnterState();
    }
    /// <summary>
    /// ��״̬�����л�ȡָ��ID��״̬
    /// </summary>
    /// <param name="stateId">ָ����״̬ID</param>
    /// <returns>��ȡ��״̬</returns>
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

    public void ForceChangeCurState(StateBase state)//ǿ��ת��״̬
    {
        ChangeState(CurrentState, state);
    }

    public void ForceStop()//ǿ��״̬���˳�
    {
        ForceChangeCurState(exitState);
    }

    //public bool isStop => currentState == exitState;//״̬���Ƿ�ֹͣ


    ////״̬ע��
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

    //public void Reset()//״̬������
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