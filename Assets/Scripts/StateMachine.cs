using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public IState currentState;
    public IState previousState;


    public void ChangeState(IState newState)
    {

        if (currentState != null)
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
        currentState.SetParentMachine(this);
        currentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
		if (currentState != null){
            IState state = currentState.Execute();
            if (state != null){
                ChangeState(state);
            }
        }
    }

	public void ChangeToPrevious()
	{
		currentState.Exit();
		currentState = previousState;
		currentState.Enter();
	}
}
