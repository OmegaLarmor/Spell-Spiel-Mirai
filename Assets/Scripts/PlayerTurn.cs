using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState
{

    public static System.Action startRecordingEvent;
    public static System.Action stopRecordingEvent;


    private StateMachine machine;

    public BooleanVariable boolVar; //tells others if it is playerTurn or not



    public void Enter()
    {
        BattleController.instance.voice.shareRecognitionEvent += BattleController.instance.TrySpellString;
        BattleController.instance.enemy.Die += EndBattleWin;
        boolVar = BattleController.instance.isPlayerTurn;

        BattleController.instance.battleText.text = BattleController.instance.player.name + "の番です！よく言ってね！";

        //Generate new forced spell
        BattleController.instance.mustBeCast = BattleController.instance.ChooseRandomSpell(BattleController.instance.player);

    }

    public IState Execute()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)){

            if (startRecordingEvent != null) startRecordingEvent();
            BattleController.instance.player.animator.SetBool("Casting",true);

        }
        else if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl)){

            if (stopRecordingEvent != null) stopRecordingEvent();

        }
       
        
        return null; //if we reach this, stay in current state
    }

    public void Exit()
    {
        BattleController.instance.voice.shareRecognitionEvent -= BattleController.instance.TrySpellString;
        BattleController.instance.enemy.Die -= EndBattleWin;
    }

    public void ChangeTurn(){
        machine.ChangeState(new EnemyTurn());
        boolVar.value = false; //toggles UI with outside bool object

    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }

//////////////////////////////////////////////////////////////////////

    private void EndBattleWin(){
        machine.ChangeState(new BattleWinState());
    }

}
