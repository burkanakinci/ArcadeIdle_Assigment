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
        //Debug.Log("MovingEnter");
        movementStateMachine.helperController.PlayWalkAnimation();
        //Find target
        
    }

    public override void UpdateLogic()
    {
        Debug.Log("MovingUpdateLogic");
        if(movementStateMachine.HasTargetCube())
        {
            movementStateMachine.SetTargetVelocity();
            if(movementStateMachine.rigidbodyHelper.velocity!=Vector3.zero)
            {
                movementStateMachine.transform.LookAt(movementStateMachine.transform.position+
                       movementStateMachine.rigidbodyHelper.velocity);
            }
        }
        else
        {
            movementStateMachine.ChangeState(movementStateMachine.idleState);
        }
    }
    public override void UpdatePhysics()
    {
        //Debug.Log("MovingUpdateLogic");
        movementStateMachine.rigidbodyHelper.velocity =
                movementStateMachine.tempVelocity*movementStateMachine.moveSpeed;
    }
}
