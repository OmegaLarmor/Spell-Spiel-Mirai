using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public static System.Action startRecordingEvent;
    public static System.Action stopRecordingEvent;

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
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)){

            if (startRecordingEvent != null) startRecordingEvent();

        }
        else if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift)){

            if (stopRecordingEvent != null) stopRecordingEvent();
            Debug.Log("Released");

        }
       
        
        return null; //if we reach this, stay in current state
    }

    public void Exit()
    {
        Debug.Log("Player turn ended");
    }

}
