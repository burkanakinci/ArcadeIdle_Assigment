using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;
    private void Start()
    {
        currentState = GetInitialState();
        if(currentState!=null)
        {
            currentState.Enter();
        }
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    public void ChangeState(BaseState nextState)
    {
        currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
