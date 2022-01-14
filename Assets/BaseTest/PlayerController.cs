using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerAudio playerAudio;
    PlayerAnimator playerAnimator;
    FSMBase playerFSM;
    /// <summary>
    /// 初始化变量
    /// </summary>
    private void Initialize()
    {
        playerInput = new PlayerInput();
        playerAudio = new PlayerAudio();
        playerAnimator = new PlayerAnimator();
        
        playerFSM = new FSMBase(gameObject);
        playerFSM.SetDefaultState(new Player_Idle(playerFSM, PlayerState.Player_Idle));
        
    }

    void Awake()
    {
        Initialize();
    }
    private void Start()
    {
        ////Assembly assembly = Assembly.GetExecutingAssembly();
        //Type ty = Type.GetType("Player_Attack");

        //StateBase state = (StateBase)Activator.CreateInstance(ty, new object[]{playerFSM,PlayerState.Player_Attack});
        //string name = PlayerState.Player_Attack.ToString();
        
    }
    private void Update()
    {
        playerFSM.UpdateFSM(playerInput);
    }



}
