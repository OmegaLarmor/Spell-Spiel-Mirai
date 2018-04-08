using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    public static BattleController instance = null;

    public Character player;
    public Character enemy;

	private StateMachine stateMachine;
	public static IState playerTurn = new PlayerTurn();
	public static IState enemyTurn = new EnemyTurn();
    public BooleanVariable isPlayerTurn; //kinda hacky, but very useful to toggle UI

    public VoiceHandler voice;
    private Dictionary<string, Spell> spellWordPairs = new Dictionary<string, Spell>();
    public Text battleText;

    public Spell fireball;
    public Spell waterGun;

    void Awake(){

        //Join the dark side
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Destroy(this);
        }
    }

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        spellWordPairs.Add("火玉", fireball);
        spellWordPairs.Add("水玉", waterGun);

        PlayerTurn.startRecordingEvent += StartRecording;
        PlayerTurn.stopRecordingEvent += StopRecording;

		stateMachine.ChangeState(playerTurn); //Start at player turn by default
        InitializeBattleVars();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            TrySpellString("火玉");
        }

        stateMachine.ExecuteStateUpdate();

    }

    public void StartRecording(){
        voice.StartRecording();
    }
    public void StopRecording(){
        voice.StopRecording();
    }

///////////////////////////////////////////////////////

    public void TrySpellString(string transcript)
    {
        if (transcript == "Failed"){
            Debug.Log("Voice recognition failed. Ignoring cast...");
            return;
        }
         
        Spell theSpell = null;
        
        for (int i = 0; i < player.spells.Length; i++){
            theSpell = player.spells[i].CheckInWords(transcript);
            if (theSpell != null) break;
        }

        if (theSpell == null){
            Debug.Log("I got nothing... Might wanna add " + transcript + "!");
            battleText.text = "なにもおこらなかった．．．";
            return;
        }

        //we did it! We said a valid spell! Now, we cast it.
        else{
            CastSpell(theSpell, player, enemy); //cast x from a to b
        }
    }

    public void CastSpell(Spell spell, Character caster, Character target)
    {
        Instantiate(spell);
        enemy.HandleSpell(spell, player);
        StartCoroutine(WaitForSpellEndAndChangeTurn(spell));
        battleText.text = caster.name + "は" + spell.trueName + "をつかった！";
    }

    IEnumerator WaitForSpellEndAndChangeTurn(Spell spell){
		yield return new WaitForSeconds(spell.getDuration());
		stateMachine.currentState.ChangeTurn();
		yield break;
    }

    //Resets the Scriptable objects to their required value
    public void InitializeBattleVars(){

        player.currentHP.variable.value = player.maxHP.value;
        enemy.currentHP.variable.value = enemy.maxHP.value;
        isPlayerTurn.value = true;

    }

}
