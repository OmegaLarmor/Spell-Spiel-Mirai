using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    public void Enter()
    {
        Debug.Log("Enemy turn start!");
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("I chose my option with X!");
        }
    }

    public void Exit()
    {
        Debug.Log("Enemy turn ended");
    }
}
