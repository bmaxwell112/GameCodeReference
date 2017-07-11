using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour {

	public Camera myCamera;
	
	private GameObject defenderParent;
	private StarDisplay starDisplay;
	
	void Start() {
		defenderParent = GameObject.Find("Defenders");
		starDisplay = GameObject.FindObjectOfType<StarDisplay>();
		if (!defenderParent){
			defenderParent = new GameObject("Defenders");
		}
	}
	
	void OnMouseDown () {
		Vector2 rawPos = CalculateWorldPointOfMouseClick();
		Vector2 roundedPos = SnapToGrid(rawPos);
		GameObject defender = Button.selectedDefender;
		if(defender != null){
			int defenderCost = defender.GetComponent<Defenders>().starCost;
			if(starDisplay.UseStars(defenderCost) == StarDisplay.Status.SUCCESS){
				SpawnDefender (roundedPos, defender);
			} else {
				Debug.Log ("Insufficient stars");
			}
		}
	}

	void SpawnDefender (Vector2 roundedPos, GameObject defender)
	{
		Quaternion zeroRot = Quaternion.identity;
		GameObject newDefender = Instantiate (defender, roundedPos, zeroRot) as GameObject;
		newDefender.transform.parent = defenderParent.transform;
	}
	
	Vector2 SnapToGrid (Vector2 rawWoldPos){
		float newX = Mathf.RoundToInt(rawWoldPos.x);
		float newY = Mathf.RoundToInt(rawWoldPos.y);
		return new Vector2 (newX, newY);
	}
	
	Vector2 CalculateWorldPointOfMouseClick (){
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;
		
		Vector3 weirdTriplet = new Vector3 (mouseX, mouseY, distanceFromCamera);
		Vector2 worldPos = myCamera.ScreenToWorldPoint (weirdTriplet);
		
		
		return worldPos;
	}
}
