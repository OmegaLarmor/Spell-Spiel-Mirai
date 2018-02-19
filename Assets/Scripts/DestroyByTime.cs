using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float lifetime;

	public bool killAfterAnim = false;

	// Use this for initialization
	void Start () {

		if (killAfterAnim){
			lifetime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
			lifetime -= 0.1f; //lifehack, avoid the loop first frame
		}

        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
