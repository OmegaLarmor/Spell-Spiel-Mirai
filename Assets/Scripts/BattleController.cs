using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    private bool isPlayerTurn = true;

    private Dictionary<string, GameObject> spellWordPairs = new Dictionary<string, GameObject>();

    public VoiceHandler voice;


    public GameObject fireball;
    public GameObject waterGun;

    // Use this for initialization
    void Start()
    {
        if (voice != null)
        {
            voice.shareRecognitionEvent += CastSpell;
        }

        spellWordPairs.Add("火玉", fireball);
        spellWordPairs.Add("水玉", waterGun);
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Player turn done");
                isPlayerTurn = !isPlayerTurn;
            }
        }
        else if (!isPlayerTurn)
        {

        }

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
