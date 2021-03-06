﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	public float levelSeconds = 120f;
	
	private Slider slider;
	private AudioSource audioSource;
	private bool isEndOfLevel = false;
	private LevelManager levelManager;
	private GameObject winLabel;
	
	void Start () {
		slider = GetComponent<Slider>();
		audioSource = GetComponent<AudioSource>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		FindYouWin ();
		winLabel.SetActive(false);
	}

	void FindYouWin ()
	{
		winLabel = GameObject.Find("You Win");
		if (!winLabel) {
			Debug.LogWarning ("Please create You Win object");
		}
	}

	void Update (){
		slider.value = Time.timeSinceLevelLoad / levelSeconds;
		
		if (Time.timeSinceLevelLoad >= levelSeconds && !isEndOfLevel){
			HandleWinCondition ();
		}
	}	

	void HandleWinCondition ()
	{
		DestroyAllTaggedObjects();
		audioSource.Play ();
		winLabel.SetActive (true);
		Invoke ("LoadNextLevel", audioSource.clip.length);
		isEndOfLevel = true;
	}
	
	void DestroyAllTaggedObjects(){
		GameObject[] taggedObjectArray = GameObject.FindGameObjectsWithTag ("destroyOnWin");
		foreach(GameObject taggedObject in taggedObjectArray){
			Destroy (taggedObject);
		}
	}
	
	void LoadNextLevel(){
		levelManager.LoadNextLevel();
	}
}
