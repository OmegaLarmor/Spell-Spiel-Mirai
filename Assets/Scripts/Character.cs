using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public FloatReference maxHP;
	public FloatReference currentHP;
	public FloatReference attack;
	public FloatReference defense;

	private System.Action<int,int> TookDamageEvent;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HandleSpell(Spell spell, Character caster) {

		float attackBoost = caster.attack.value;
		float defenseBoost = caster.defense.value;
		float toDeal = spell.baseDamage + attackBoost - defenseBoost;
		if (toDeal < 0) toDeal = 0;
		TakeDamage(toDeal);

	}

	public void TakeDamage(float dmg){
		currentHP.variable.value -= dmg;
	}
}
