using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private IState currentState;
    private IState previousState;


    public void ChangeState(IState newState)
    {

        if (currentState != null)
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
    }

    public void ExecuteStateUpdate()
    {
		if (currentState != null) currentState.Execute();
    }

	public void ChangeToPrevious()
	{
		currentState.Exit();
		currentState = previousState;
		currentState.Enter();
	}
}
