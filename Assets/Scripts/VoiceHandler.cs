using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition; //we cool

public class VoiceHandler : MonoBehaviour
{

    private GCSpeechRecognition _speechRecognition;

    public bool _isRuntimeDetectionToggle;

    public Text _speechRecognitionResult; //to update on screen

    public System.Action<string> shareRecognitionEvent;

    public bool isRecording = false;
    public bool isWaiting = false;
    public float maxWaitTime = 5;

    // Use this for initialization
    void Start()
    {
        _speechRecognition = GCSpeechRecognition.Instance;
		_speechRecognition.SetLanguage(Enumerators.LanguageCode.ja_JP);

        //Adds functions to be called for each event
        _speechRecognition.RecognitionSuccessEvent += RecognitionSuccessEventHandler;
        _speechRecognition.NetworkRequestFailedEvent += SpeechRecognizedFailedEventHandler;
        _speechRecognition.LongRecognitionSuccessEvent += LongRecognitionSuccessEventHandler;

    }

	private void OnDestroy()
	{
		_speechRecognition.RecognitionSuccessEvent -= RecognitionSuccessEventHandler;
		_speechRecognition.NetworkRequestFailedEvent -= SpeechRecognizedFailedEventHandler;
		_speechRecognition.LongRecognitionSuccessEvent -= LongRecognitionSuccessEventHandler;
	}

	public void StartRecording()
        {
            if(isRecording || isWaiting){
                Debug.Log("Hey! We can't record now!");
                return;
            }
            isRecording = true;
            isWaiting = false;

            _speechRecognitionResult.text = "";
            _speechRecognition.StartRecord(_isRuntimeDetectionToggle);

            StartCoroutine(LimitRecordTime(maxWaitTime));
        }

	public void StopRecording()
	{
		//ApplySpeechContextPhrases(); Not sure if needed/useful

        if(!isRecording || isWaiting){
            Debug.Log("Hey! We can't stop recording now!");
            return;
        }
        isRecording = false;
        isWaiting = true;

		_speechRecognition.StopRecord();
	}

    private void SpeechRecognizedFailedEventHandler(string obj, long requestIndex)
    {
        isWaiting = false;
        //_speechRecognitionResult.text = "Speech Recognition failed with error: " + obj;
        if (shareRecognitionEvent != null){
            shareRecognitionEvent("Failed");
        }
    }

    private void RecognitionSuccessEventHandler(RecognitionResponse obj, long requestIndex)
    {
        isWaiting = false;

        if (obj != null && obj.results.Length > 0)
        {
            string bestFit = obj.results[0].alternatives[0].transcript;
            //_speechRecognitionResult.text = "Word heard: " + bestFit;

            Debug.Log("Called RecognitionSuccessHandler");

            if (shareRecognitionEvent != null){
                shareRecognitionEvent(bestFit);
            }
        }
        else
        {
           // _speechRecognitionResult.text = "Speech Recognition succeeded! But no words recognised... :( ";
        }
    }

    //Not currently usable, todo?
    private void LongRecognitionSuccessEventHandler(OperationResponse operation, long index)
    {

        if (operation != null && operation.response.results.Length > 0)
        {
            _speechRecognitionResult.text = "Long Speech Recognition succeeded! Detected Most useful: " + operation.response.results[0].alternatives[0].transcript;

            //Loop of death and sorrow. May this be recorded in history, and shared through generations.
            /* 
            string other = "\nDetected alternative: ";

            foreach (var result in obj.results)
            {
                foreach (var alternative in result.alternatives)
                {
                    if (obj.results[0].alternatives[0] != alternative)
                        other += alternative.transcript + ", ";
                }
            }*/

            //_speechRecognitionResult.text += other;
            //_speechRecognitionResult.text += "\nTime for the recognition: " +
            //    (operation.metadata.lastUpdateTime - operation.metadata.startTime).TotalSeconds + "s";
        }
        else
        {
            _speechRecognitionResult.text = "Speech Recognition succeeded! Words are no detected.";
        }
    }

    IEnumerator LimitRecordTime(float maxTime){

        yield return new WaitForSeconds(maxTime); //Let the record happen...

        if (isRecording){
            Debug.Log("That's enough! Cut the audio!");
            StopRecording(); //That's enough! Cut the audio!
        }
    }
}
