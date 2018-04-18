using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLossState : IState {

	private StateMachine machine;

	public void Enter(){

		BattleController.instance.player.animator.SetBool("Dead", true);

		//No progress increment here

		BattleController.instance.battleText.text = "だめ！" + BattleController.instance.player.name + "がたおされた！";

	}

	public void Exit(){

	}

	public IState Execute(){

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
			SceneManager.LoadScene("BattleScene");
		}

		return null;

	}

	public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }

	//It's in the interface for technical reasons
	public void ChangeTurn(){

	}

}
