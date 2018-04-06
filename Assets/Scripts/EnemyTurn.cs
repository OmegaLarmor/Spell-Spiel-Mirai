using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    private StateMachine machine;

    public BooleanVariable boolVar; //tells others if it is playerTurn or not

    public void Enter()
    {
        Debug.Log("Enemy turn start!");
        BattleController.instance.player.animator.SetBool("Casting", false);
        boolVar = BattleController.instance.isPlayerTurn;
    }

    public IState Execute()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            return BattleController.playerTurn;
        }
        else return null;
    }

    public void Exit()
    {
        Debug.Log("Enemy turn ended");
    }

    public void ChangeTurn(){
        machine.ChangeState(new PlayerTurn());
        boolVar.value = false; //toggles UI with outside bool object
    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }
}
