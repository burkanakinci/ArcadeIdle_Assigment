using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine
{
    public HelperController helperController;
    [HideInInspector] public Vector3 tempVelocity;
    [SerializeField] private float speed=5f;

    [HideInInspector] public Moving movingState;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public MovingStorage movingStorageState;

    public Rigidbody rigidbodyHelper;
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
    public void SetTargetVelocity()
    {
        tempVelocity = Vector3.Normalize((helperController.GetLastCubePos()- transform.position));
    }
    public bool HasCollectedCube()
    {
        if(helperController.GetCollectedCubeCount()>0)
        {
            return true;
        }
        return false;
    }
    public bool HasTargetCube()
    {
        if(helperController.GetTargetCubeCount() > 0)
        {
            return true;
        }
        return false;
    }
}
