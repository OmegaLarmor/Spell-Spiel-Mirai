using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flicker : MonoBehaviour {

	//Currently only works for GOs with image components that must flicker

	public float waitBeforeStart;
	public float waitBetweenFlickers;
	public int flickerNumber;

	public bool loop;

	private Image image;

	// Use this for initialization
	void Start () {

		image = GetComponent<Image>();
		StartCoroutine(StartFlicker());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator StartFlicker ()
    {
		Debug.Log("Start Flicker");
		yield return new WaitForSeconds(waitBeforeStart);

        bool Switch = false;
        for (int i = 0; i < flickerNumber; i++)
        {
			Debug.Log("Loop");
            if (Switch == true)
            {
				image.enabled = false;
                Switch = false;
            }
            else
            {
				image.enabled = true;
                Switch = true;
            }
            if (loop){
                i--; //retourne en arrière
            }
            yield return new WaitForSeconds(waitBetweenFlickers);
        }
    }
}
