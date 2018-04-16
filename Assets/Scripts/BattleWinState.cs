using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleWinState : IState {

	private StateMachine machine;

	public void Enter(){
		
		BattleController.instance.player.animator.SetBool("Casting", false);

		BattleController.instance.progress.variable.value++;

		BattleController.instance.battleText.text = "やった！" + BattleController.instance.player.name + "がかちぬけたよ！";
		BattleController.instance.battleText.GetComponent<Flicker>().enabled = true;


	}

	public void Exit(){

		BattleController.instance.battleText.GetComponent<Flicker>().enabled = false;

	}

	public IState Execute(){

		if (Input.GetKeyDown(KeyCode.Space)){

			if (BattleController.instance.progress.variable.value > BattleController.instance.progress.maxValue){
				SceneManager.LoadScene("EndSequence");
			}

			else SceneManager.LoadScene("BattleScene");
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
