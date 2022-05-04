using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine
{
    public HelperController helperController;
    [HideInInspector] public Vector3 tempVelocity;
    [HideInInspector] public Moving movingState;
    [HideInInspector] public Idle idleState;
    public float moveSpeed = 5f;

    private void Awake()
    {
        movingState = new Moving(this);
        idleState = new Idle(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
    public bool HasCollectedCube()
    {
        if (helperController.GetCollectedCubeCount() > 0)
        {
            return true;
        }
        return false;
    }
    public bool HasTargetCube()
    {
        if (helperController.GetTargetCubeCount() > 0)
        {
            return true;
        }
        return false;
    }

    public void SetTarget()
    {
        helperController.SetNavMeshTarget();
    }
}
