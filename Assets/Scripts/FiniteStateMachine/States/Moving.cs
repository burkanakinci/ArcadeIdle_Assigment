using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private MovementStateMachine movementStateMachine;

    public Moving(MovementStateMachine stateMachine) : base("Moving", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {
        //base.Enter();
       
        movementStateMachine.helperController.PlayWalkAnimation();

        movementStateMachine.SetTarget();
        //Find target

    }
}
