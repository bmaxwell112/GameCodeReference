using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource audioSource;
	
	void Awake(){
		DontDestroyOnLoad (gameObject);

        Debug.Log("Don't Destroy on load " + name);
	}
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}
	
	void OnLevelWasLoaded (int level) {
		AudioClip thisLevelMusic = levelMusicChangeArray[level];
		
		
		if(thisLevelMusic){
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("playing clip " + thisLevelMusic);
        }
	}
	
	public void ChangeVolume (float volume){
		audioSource.volume = volume;
	}
}
