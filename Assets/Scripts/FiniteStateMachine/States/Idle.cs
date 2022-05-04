using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{

    private MovementStateMachine movementStateMachine;

    public Idle(MovementStateMachine stateMachine) : base("Idle", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {
        //base.Enter();
        //Debug.Log("IdleEnter");
        movementStateMachine.helperController.PlayIdleAnimation();

    }

    // public override void UpdateLogic()
    // {
    //     if (movementStateMachine.HasTargetCube())
    //     {
    //         movementStateMachine.ChangeState(movementStateMachine.movingState);
    //     }
    // }
}
