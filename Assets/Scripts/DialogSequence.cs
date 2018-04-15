using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return)) {

			AdvanceText();

		}
	}

	void AdvanceText(){

		if (currentIndex == segments.Count - 1){
			SceneManager.LoadScene("BattleScene");
			return;
		}

		currentIndex++;
		UIText.text = segments[currentIndex];

	}
}
