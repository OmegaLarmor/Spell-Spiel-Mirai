using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWinState : IState {

	private StateMachine machine;

	public void Enter(){
		
		BattleController.instance.player.animator.SetBool("Casting", false);
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
