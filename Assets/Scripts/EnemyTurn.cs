using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    private StateMachine machine;

    public void Enter()
    {
        Debug.Log("Enemy turn start!");
        BattleController.instance.player.animator.SetBool("Casting", false);
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
    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }
}
