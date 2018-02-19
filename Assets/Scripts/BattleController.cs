using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    private bool isPlayerTurn = true;
	private StateMachine stateMachine;
	private IState playerTurn = new PlayerTurn();
	private IState enemyTurn = new EnemyTurn();

    public VoiceHandler voice;
    private Dictionary<string, GameObject> spellWordPairs = new Dictionary<string, GameObject>();

    public GameObject fireball;
    public GameObject waterGun;

    // Use this for initialization
    void Start()
    {
		stateMachine = GetComponent<StateMachine>();
        if (voice != null)
        {
            voice.shareRecognitionEvent += CastSpell;
        }

        spellWordPairs.Add("火玉", fireball);
        spellWordPairs.Add("水玉", waterGun);

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

}
