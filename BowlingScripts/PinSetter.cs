using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public GameObject pinSet;

	private PinCounter pinCounter;
	private Animator animator;

	// Use this for initialization
	void Start () {
		pinCounter = GameObject.FindObjectOfType<PinCounter>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame



	void OnTriggerExit(Collider collider)
	{
		GameObject thingLeaving = collider.gameObject;

		if (thingLeaving.GetComponent<Pin>())
		{
			Destroy(collider.gameObject);
		}
	}

	public void RaisePins() {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
		{
			pin.RaiseIfStanding();
		}
	}

	public void LowerPins() {
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
		{			
			pin.Lower();
		}
	}

	public void RenewPins()
	{
		Debug.Log("Renews pins");
		GameObject newPins = Instantiate(pinSet);
		newPins.transform.position += new Vector3(0, 40, 0);
	}

	public void PerformAction(ActionMasterOld.Action action)
	{
		if (action == ActionMasterOld.Action.Tidy)
		{
			animator.SetTrigger("tidyTrigger");
		}
		else if (action == ActionMasterOld.Action.EndTurn)
		{
			animator.SetTrigger("ResetTrigger");
			pinCounter.Reset();
		}
		else if (action == ActionMasterOld.Action.Reset)
		{
			animator.SetTrigger("ResetTrigger");
			pinCounter.Reset();
		}
		else if (action == ActionMasterOld.Action.EndGame)
		{
			throw new UnityException("Don't know how to handle end game yet");
		}
	}
}
