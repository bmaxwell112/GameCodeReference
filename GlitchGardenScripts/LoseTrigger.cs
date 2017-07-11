using UnityEngine;
using System.Collections;

public class LoseTrigger : MonoBehaviour {

	private Attacker attacker;
	private LevelManager levelManager;
	
	void Start (){
		//attacker = GameObject.FindObjectOfType<Attacker>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		levelManager.LoadLevel("03b Lose");
	}
}
