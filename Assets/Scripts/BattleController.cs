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
    public Text battleText;

    public Spell mustBeCast;
    public Sprite disableBox;
    public Sprite enableBox;
    public Transform spellUI;

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

        PlayerTurn.startRecordingEvent += StartRecording;
        PlayerTurn.stopRecordingEvent += StopRecording;

		stateMachine.ChangeState(playerTurn); //Start at player turn by default
        InitializeBattleVars();
    }

    void Update()
    {

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
        bool rebel = false; //if we didn't say the spell that was asked
        
        for (int i = 0; i < player.spells.Length; i++){
            theSpell = player.spells[i].CheckInWords(transcript);
            if (theSpell != null) break;
        }

        if (theSpell != mustBeCast){

            Debug.Log("That's not the spell I wanted! But you can cast a worse version I guess...");
            rebel = true;

        }

        if (theSpell == null){
            Debug.Log("I got nothing... Might wanna add " + transcript + "!");
            battleText.text = "なにもおこらなかった．．．";
            return;
        }

        //we did it! We said a valid spell! Now, we cast it.
        else{
            CastSpell(theSpell, player, enemy, rebel : rebel); //cast x from a to b
        }
    }

    public void CastSpell(Spell spell, Character caster, Character target, bool rebel = false)
    {
        //Handle the instantiation
        float spawnX = spell.spawnAtUser ? caster.transform.position.x : target.transform.position.x;
        float spawnY = spell.spawnAtUser ? caster.transform.position.y : target.transform.position.y;
        float spawnFlip = spell.mustFlip && !(isPlayerTurn.value) ? 180 : 0;
        Instantiate(spell, new Vector3(spawnX,spawnY,0), Quaternion.Euler(0,0,spawnFlip));

        target.HandleSpell(spell, caster, rebel);
        StartCoroutine(WaitForSpellEndAndChangeTurn(spell, paddingEnd : 1));
        battleText.text = caster.name + "は" + spell.trueName + "をつかった！";
    }

    public Spell ChooseRandomSpell(Character caster) {

        int spellLength = caster.spells.Length;
        
        if (spellLength == 0)
        {
            return null;
        }
        int i = Random.Range(0, spellLength); //pick random spell (I am an AI coder)

        CloseAllSpellUIExceptIndex(i);

        return caster.spells[i];

    }


    /// Only used by Enemy!
    public IEnumerator CastAfterSeconds(float secs, Spell spell){
        yield return new WaitForSeconds(secs);
        CastSpell(spell, enemy, player);
        yield break;
    }///

    IEnumerator WaitForSpellEndAndChangeTurn(Spell spell, float paddingEnd = 0){
		yield return new WaitForSeconds(spell.getDuration() + paddingEnd);
		stateMachine.currentState.ChangeTurn();
		yield break;
    }

    //Resets the Scriptable objects to their required initial value
    public void InitializeBattleVars(){

        player.currentHP.variable.value = player.maxHP.value;
        enemy.currentHP.variable.value = enemy.maxHP.value;
        enemy.maxHP.variable.value = enemy.maxHP.value; //in case we're using constant value
        isPlayerTurn.value = true;

    }

    //Sinful
    public void CloseAllSpellUIExceptIndex(int index){

        //Transform spellUI = GameObject.Find("SpellUI").transform; //wait for it
        Image[] images = spellUI.GetComponentsInChildren<Image>(); //wait for it

        for (int i = 0; i < spellUI.childCount; i++){

            if (images[i] != null){

                if (i != index){
                
                images[i].sprite = disableBox;
                }

                else if (i == index ){
                    images[i].sprite = enableBox;
                }
            }                      
        }

    }

    public void EndBattleWin(){
        
        battleText.text = player.name + "が" + enemy.name + "をたおした!";
    }

}
