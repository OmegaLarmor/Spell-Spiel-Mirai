using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    public static BattleController instance = null;

    public Character player;
    public Character enemy;

	private StateMachine stateMachine;
	public static IState playerTurn = new PlayerTurn();
	public static IState enemyTurn = new EnemyTurn();

    public VoiceHandler voice;
    private Dictionary<string, Spell> spellWordPairs = new Dictionary<string, Spell>();

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
            CastSpell("火玉");
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

    public void CastSpell(string transcript)
    {
        if (transcript == "Failed"){
            Debug.Log("Voice recognition failed. Ignoring cast...");
            return;
        }
        Debug.Log("Spell cast: " + transcript);
        try
        {
            Spell theSpell = spellWordPairs[transcript];

            //todo: limit to a single instance to prevent blasting when error
            Instantiate(theSpell);
            enemy.HandleSpell(theSpell, player);
            StartCoroutine(WaitForSpellEndAndChangeTurn(theSpell));
        }

		catch (KeyNotFoundException){
			Debug.Log("Transcript was not in dict");
		}
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

    }

}
