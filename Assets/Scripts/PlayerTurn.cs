using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public static System.Action startRecordingEvent;
    public static System.Action stopRecordingEvent;


    private StateMachine machine;



    public void Enter()
    {
        BattleController.instance.voice.shareRecognitionEvent += BattleController.instance.CastSpell;
        //BattleController.instance.voice.shareRecognitionEvent += IfSuccessYieldTurn;
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
            BattleController.instance.player.animator.SetBool("Casting",true); //Gets 

        }
        else if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift)){

            if (stopRecordingEvent != null) stopRecordingEvent();
            //BattleController.instance.player.animator.SetBool("Casting",false);

        }
       
        
        return null; //if we reach this, stay in current state
    }

    public void Exit()
    {
        Debug.Log("Player turn ended");
        BattleController.instance.voice.shareRecognitionEvent -= BattleController.instance.CastSpell;
        BattleController.instance.enemy.Die -= EndBattleWin;
    }

    public void ChangeTurn(){
        machine.ChangeState(new EnemyTurn());
    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }

//////////////////////////////////////////////////////////////////////

    private void EndBattleWin(){
        Debug.Log("We won!");
    }

}
