using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    private StateMachine machine;

    private float enemyThinkTime = 2; //In seconds. So that we don't insta-cast like a robot ;)

    public BooleanVariable boolVar; //tells others if it is playerTurn or not

    private Spell toCast; //chosen by AI

    public void Enter()
    {
        BattleController.instance.battleText.text = BattleController.instance.enemy.name + "は考えている。。。";
        BattleController.instance.player.animator.SetBool("Casting", false);

        BattleController.instance.player.Die += EndBattleLoss;
        boolVar = BattleController.instance.isPlayerTurn;

        toCast = BattleController.instance.ChooseRandomSpell(BattleController.instance.enemy);

        if (toCast == null) {
            Debug.Log("I didn't have any spells to cast, so you can have your turn back...");
        }

        BattleController.instance.StartCoroutine(BattleController.instance.CastAfterSeconds(enemyThinkTime, toCast)); //why is StartCoroutine a MonoBehaviour method.... :/
    }

    public IState Execute()
    {
        return null;
    }

    public void Exit()
    {
        BattleController.instance.player.Die -= EndBattleLoss;
    }

    public void ChangeTurn(){

        boolVar.value = true; //toggles UI with outside bool object
        machine.ChangeState(new PlayerTurn());
    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }
    ////////////////////////////////////////////////////

    public void EndBattleLoss(){

        machine.ChangeState(new BattleLossState());
    }

}
