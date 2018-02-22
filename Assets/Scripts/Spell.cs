using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public float baseDamage = 1;

    private Animator animator;
    private float duration; //to deal damage after animation

    // Use this for initialization
    void Start()
    {

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
}
