using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    Player_Idle,
    Player_Attack,
    Player_Run,
    Player_Jump,
    Player_Move

}
public class StateBase 
{

    public Enum StateId { get; private set; }//状态ID
    public FSMBase ParentFSM { get; private set; }//状态归属哪个状态机
    
    //状态时间
    public float StartTime { get; private set; }
    public float RunTime { get; private set; }

    //状态可以不用回调，因为状态机的状态改变回调给了两个状态参数

    public bool Active { get; set; } = true;//状态是否激活
    public StateBase(FSMBase ParentFSM, Enum StateId)
    {
        this.ParentFSM = ParentFSM;
        this.StateId = StateId;
    }

    public virtual void EnterState()
    {//进入动作，可以对状态做初始化；初始化也可以在生成状态类实例时做
     //前者初始化的方式可能与运行时参数有关，后者无关
        StartTime = Time.time;
    }
    public virtual void ExitState()//退出动作，可以对状态对重置
    {
        Debug.Log($"Runtime:{RunTime}");
        RunTime = 0;
    }

    //在这个状态内要执行的操作，可以有多个
    public virtual void UpdateState()//在状态内执行相关输出
    {
        RunTime += Time.deltaTime;
    }

    public virtual StateBase InputNextState(PlayerInput input)//在当前状态内处理输入
    {
        return null;
    }
}

class Player_Idle:StateBase
{
    public Player_Idle(FSMBase parentFSM, PlayerState stateId):base(parentFSM, stateId)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("进入Idle了");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("更新Idle");
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("退出Idle了");
    }


    public override StateBase InputNextState(PlayerInput input)
    {
        if (input.Attack)
            return ParentFSM.GetState(PlayerState.Player_Attack);
        else if (input.Run)
            return ParentFSM.GetState(PlayerState.Player_Run);
        else if (input.Jump)
            return ParentFSM.GetState(PlayerState.Player_Jump);
        else if (input.Move())
            return ParentFSM.GetState(PlayerState.Player_Move);
        return base.InputNextState(input);
    }
}
class Player_Attack : StateBase
{
    public Player_Attack(FSMBase parentFSM, PlayerState stateId) : base(parentFSM, stateId)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("进入Attack了");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("退出Attack了");
    }

    public override StateBase InputNextState(PlayerInput input)
    {
        return base.InputNextState(input);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("更新Attack");
    }
}
class Player_Jump : StateBase
{
    /* #region Singleton
     private static Jump instance;
     public static Jump Instance
     {
         get
         {
             return (instance ?? (instance = new Jump()));
         }
     }
     private Jump()
     {
     }
     static Jump()
     {
         instance = new Jump();
     }
     #endregion*/
    public Player_Jump(FSMBase parentFSM, PlayerState stateId) : base(parentFSM, stateId)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override StateBase InputNextState(PlayerInput input)
    {
        return base.InputNextState(input);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
class Player_Move : StateBase
{
    public Player_Move(FSMBase parentFSM, PlayerState stateId) : base(parentFSM, stateId)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override StateBase InputNextState(PlayerInput input)
    {
        if(input.Move())
            return ParentFSM.GetState(PlayerState.Player_Move);
        return base.InputNextState(input);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        ParentFSM.FSMMaster.transform.Translate(Vector3.right * Time.deltaTime * 5);
    }
}

