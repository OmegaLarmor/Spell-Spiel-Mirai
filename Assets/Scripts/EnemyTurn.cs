using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    public void Enter()
    {
        Debug.Log("Enemy turn start!");
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
}
