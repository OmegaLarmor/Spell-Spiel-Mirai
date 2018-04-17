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

	public TextAsset textFile;

	private int currentIndex = 0;

	// Use this for initialization
	void Start () {

		//var sr = new StreamReader(Application.dataPath + "/" + fileName);
		//var fileContents = sr.ReadToEnd();
		//sr.Close();
	
		segments = new List<string>();
		//segments.AddRange(fileContents.Split(new [] {"\r\n\r\n"}, StringSplitOptions.None));
		segments.AddRange(textFile.text.Split(new [] {"\r\n\r\n"}, StringSplitOptions.None));
		UIText = GetComponentInChildren<Text>();


		UIText.text = segments[0];
	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightArrow)) {

			AdvanceText();

		}

		else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.LeftArrow)){

			RewindText();

		}
	}

	void AdvanceText(){

		if (currentIndex == segments.Count - 1){

			if (SceneManager.GetActiveScene().name == "IntroSequence"){
				SceneManager.LoadScene("BattleScene");
			}

			else if (SceneManager.GetActiveScene().name == "EndSequence"){
				SceneManager.LoadScene("Credits");
			}

			return;
		}

		currentIndex++;
		UIText.text = segments[currentIndex];

	}

	void RewindText(){

		if (currentIndex == 0){
			return;
		}

		currentIndex--;
		UIText.text = segments[currentIndex];

	}
}
