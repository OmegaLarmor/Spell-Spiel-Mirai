using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flicker : MonoBehaviour
{

    //Currently only works for GOs with image components and/or text components

    public float waitBeforeStart;
    public float waitBetweenFlickers;
    public int flickerNumber;

    public bool loop;
    public bool startHidden;

    private Image image;
	private string initialText;

    public bool hasImage;
    public bool hasText;

    // Use this for initialization
    void Start()
    {

        if (hasImage) { image = GetComponent<Image>(); }
		if (hasText) { initialText = GetComponent<Text>().text;}

        StartCoroutine(StartFlicker());

        if (startHidden)
        {
            Switch(false);
        }

    }

    void OnEnable()
    {

        StartCoroutine(StartFlicker());

    }

    void OnDisable()
    {

        StopCoroutine(StartFlicker());

    }

    void Switch(bool onOff)
    {
        
		if (hasImage){
			image.enabled = onOff;
		}

		if (hasText){
			GetComponent<Text>().text = onOff ? initialText : "";
		}

    }

    IEnumerator StartFlicker()
    {
        yield return new WaitForSeconds(waitBeforeStart);

        bool toSwitch = false;
        for (int i = 0; i < flickerNumber; i++)
        {
            if (toSwitch == true)
            {
                Switch(false);
                toSwitch = false;
            }
            else
            {
                Switch(true);
                toSwitch = true;
            }
            if (loop)
            {
                i--; //retourne en arrière
            }
            yield return new WaitForSeconds(waitBetweenFlickers);
        }
    }
}
