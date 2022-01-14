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

    public Enum StateId { get; private set; }//״̬ID
    public FSMBase ParentFSM { get; private set; }//״̬�����ĸ�״̬��
    
    //״̬ʱ��
    public float StartTime { get; private set; }
    public float RunTime { get; private set; }

    //״̬���Բ��ûص�����Ϊ״̬����״̬�ı�ص���������״̬����

    public bool Active { get; set; } = true;//״̬�Ƿ񼤻�
    public StateBase(FSMBase ParentFSM, Enum StateId)
    {
        this.ParentFSM = ParentFSM;
        this.StateId = StateId;
    }

    public virtual void EnterState()
    {//���붯�������Զ�״̬����ʼ������ʼ��Ҳ����������״̬��ʵ��ʱ��
     //ǰ�߳�ʼ���ķ�ʽ����������ʱ�����йأ������޹�
        StartTime = Time.time;
    }
    public virtual void ExitState()//�˳����������Զ�״̬������
    {
        Debug.Log($"Runtime:{RunTime}");
        RunTime = 0;
    }

    //�����״̬��Ҫִ�еĲ����������ж��
    public virtual void UpdateState()//��״̬��ִ��������
    {
        RunTime += Time.deltaTime;
    }

    public virtual StateBase InputNextState(PlayerInput input)//�ڵ�ǰ״̬�ڴ�������
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
        Debug.Log("����Idle��");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("����Idle");
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("�˳�Idle��");
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
        Debug.Log("����Attack��");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("�˳�Attack��");
    }

    public override StateBase InputNextState(PlayerInput input)
    {
        return base.InputNextState(input);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("����Attack");
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

