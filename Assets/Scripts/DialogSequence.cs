using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System;

public class DialogSequence : MonoBehaviour {

	//TODO: have an event sent when we're done reading? Options for choices?

	private List<string> segments;

	public string fileName;
	public Text UIText;

	private int currentIndex = 0;

	// Use this for initialization
	void Start () {

		var sr = new StreamReader(Application.dataPath + "/" + fileName);
		var fileContents = sr.ReadToEnd();
		sr.Close();
	
		segments = new List<string>();
		segments.AddRange(fileContents.Split(new [] {"\r\n\r\n"}, StringSplitOptions.None));

		UIText = GetComponentInChildren<Text>();

		UIText.text = segments[0];
	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.Space)) {

			AdvanceText();

		}
	}

	void AdvanceText(){

		if (currentIndex == segments.Count - 1){
			Debug.Log("Box finished");
			return;
		}

		currentIndex++;
		UIText.text = segments[currentIndex];

	}
}
