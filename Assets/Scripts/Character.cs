using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public FloatReference maxHP;
	public FloatReference currentHP;
	private System.Action<int,int> TookDamageEvent;


	//public List<Spell> spellList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TakeDamage(float dmg){
		//currentHP.variable -= dmg;
		//TookDamageEvent(currentHP,maxHP);
	}
}
