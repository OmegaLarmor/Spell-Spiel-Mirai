using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLossState : IState {

	private StateMachine machine;

	public void Enter(){

	}

	public void Exit(){

	}

	public IState Execute(){

		return null;

	}

	public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }

	//It's in the interface for technical reasons
	public void ChangeTurn(){

	}

}
