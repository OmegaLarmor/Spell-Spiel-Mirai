using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public void Enter()
    {
        Debug.Log("Player turn start!");
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("I chose my option with Space!");
        }
    }

    public void Exit()
    {
        Debug.Log("Player turn ended");
    }

}
