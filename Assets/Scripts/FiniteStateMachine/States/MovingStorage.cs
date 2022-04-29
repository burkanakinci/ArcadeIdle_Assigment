using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStorage : BaseState
{

    private MovementStateMachine movementStateMachine;

    public MovingStorage(MovementStateMachine stateMachine) : base("MovingStorage", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {
        //base.Enter();
        //Debug.Log("MovingEnter");
        movementStateMachine.helperController.PlayIdleAnimation();

    }

    public override void UpdateLogic()
    {

        if (!movementStateMachine.HasCollectedCube())
        {
            movementStateMachine.ChangeState(movementStateMachine.movingState);
        }
    }
}
