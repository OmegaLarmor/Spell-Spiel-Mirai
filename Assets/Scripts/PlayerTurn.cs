using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public static System.Action startRecordingEvent;
    public static System.Action stopRecordingEvent;

    //private Dictionary<string, GameObject> spellWordPairs = new Dictionary<string, GameObject>();
    //public GameObject fireball;
    //public GameObject waterGun;



    public void Enter()
    {
        BattleController.instance.voice.shareRecognitionEvent += BattleController.instance.CastSpell;
        BattleController.instance.enemy.Die += EndBattleWin;

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

        }
       
        
        return null; //if we reach this, stay in current state
    }

    public void Exit()
    {
        Debug.Log("Player turn ended");
        BattleController.instance.voice.shareRecognitionEvent -= BattleController.instance.CastSpell;
        BattleController.instance.enemy.Die -= EndBattleWin;
    }

//////////////////////////////////////////////////////////////////////

    private void EndBattleWin(){
        Debug.Log("We won!");
    }

}
