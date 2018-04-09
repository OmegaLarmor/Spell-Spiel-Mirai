using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{

    private StateMachine machine;

    private float enemyThinkTime = 2; //In seconds. So that we don't insta-cast like a robot ;)

    public BooleanVariable boolVar; //tells others if it is playerTurn or not

    private Spell toCast; //chosen by AI
    private ArrayList spellChoices = new ArrayList();

    public void Enter()
    {
        BattleController.instance.battleText.text = BattleController.instance.enemy.name + "は考えている。。。";
        BattleController.instance.player.animator.SetBool("Casting", false);

        boolVar = BattleController.instance.isPlayerTurn;

        toCast = ChooseEnemySpell();

        if (toCast == null) {
            Debug.Log("I didn't have any spells to cast, so you can have your turn back...");
            ChangeTurn();
        }

        BattleController.instance.StartCoroutine(BattleController.instance.CastAfterSeconds(enemyThinkTime, toCast)); //why is StartCoroutine a MonoBehaviour method.... :/
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
       // Debug.Log("Enemy turn ended");
    }

    public void ChangeTurn(){
        //Debug.Log("Triggering player turn");
        machine.ChangeState(new PlayerTurn());
        boolVar.value = true; //toggles UI with outside bool object
    }

    public void SetParentMachine(StateMachine machine){
        this.machine = machine;
    }
    ////////////////////////////////////////////////////

    private Spell ChooseEnemySpell(){

        int spellLength = BattleController.instance.enemy.spells.Length;
        
        if (spellLength == 0)
        {
            return null;
        }

        int i = Random.Range(0, spellLength); //pick random spell (I am an AI coder)
        return BattleController.instance.enemy.spells[i];

    }

}
