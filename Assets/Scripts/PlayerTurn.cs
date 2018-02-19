using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public void Enter()
    {
        Debug.Log("Player turn start!");
    }

    public IState Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return BattleController.enemyTurn;
        }
        else return null;
    }

    public void Exit()
    {
        Debug.Log("Player turn ended");
    }

}
