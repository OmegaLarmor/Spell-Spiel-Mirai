﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public string trueName; //will stay public for simplicity 

    public float baseDamage = 1;

    private Animator animator;
    private float duration; //to deal damage after animation

    public bool mustFlip; //if we have to flip when the enemy casts it
    public bool selfCast; //has to do with the effect (prob won't be used)
    public bool spawnAtUser;

    public string[] wordsAllowed;
    
    // Use this for initialization
    void Start()
    {
        //image = textBox.GetComponent<Image>();
        //ToggleUIBox(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getDuration()
    {
        if (GetComponent<DestroyByTime>() != null)
        {
            duration = GetComponent<DestroyByTime>().lifetime;
        }
        else
		{
			Debug.Log("Spell has no DestroyByTime, had to insta-deal damage...");
			duration = 0;
		}
		return duration;
    }

    public Spell CheckInWords(string transcript){

        for (int i = 0; i < wordsAllowed.Length; i++){
            if (transcript == wordsAllowed[i]){
                return this;
            }
        }

        return  null; //if we get here, nothing was found :/

    }
}
