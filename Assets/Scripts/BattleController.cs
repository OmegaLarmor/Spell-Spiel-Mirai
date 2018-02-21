using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    public static BattleController instance = null;

	private StateMachine stateMachine;
	public static IState playerTurn = new PlayerTurn();
	public static IState enemyTurn = new EnemyTurn();

    public VoiceHandler voice;
    private Dictionary<string, GameObject> spellWordPairs = new Dictionary<string, GameObject>();

    public GameObject fireball;
    public GameObject waterGun;

    void Awake(){

        //Join the dark side
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
		stateMachine = GetComponent<StateMachine>();

        spellWordPairs.Add("火玉", fireball);
        spellWordPairs.Add("水玉", waterGun);

        PlayerTurn.startRecordingEvent += StartRecording;
        PlayerTurn.stopRecordingEvent += StopRecording;

		stateMachine.ChangeState(playerTurn); //Start at player turn by default
    }

    // Update is called once per frame
    void Update()
    {

        stateMachine.ExecuteStateUpdate();

    }

    public void CastSpell(string transcript)
    {
        Debug.Log("Spell cast: " + transcript);
        try
        {
            Instantiate(spellWordPairs[transcript]);
        }

		catch (KeyNotFoundException){
			Debug.Log("Transcript was not in dict");
		}
    }

    public void StartRecording(){
        voice.StartRecording();
    }
    public void StopRecording(){
        voice.StopRecording();
    }

}
