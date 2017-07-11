using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

	public Text text;

	private GameManager gameManager;
	private int lastStandingCount = -1;
	private bool ballOutOfPlay = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;

	void Start()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	void Update()
	{
		text.text = CountStanding().ToString();

		if (ballOutOfPlay)
		{
			UpdateStandingCountAndSettle();
			text.color = Color.red;
		}
	}

	public int CountStanding()
	{
		int standing = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
		{
			if (pin.IsStanding())
			{
				standing++;
			}
		}
		return standing;
	}

	void UpdateStandingCountAndSettle()
	{
		// Update the Last Standing Count
		// Call PinsHaveSettled () once they have.
		int currentStanding = CountStanding();
		if (currentStanding != lastStandingCount)
		{
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f;
		if ((Time.time - lastChangeTime) > settleTime)
		{ // If last change is > 3s ago
			PinsHaveSettled();
		}
	}

	void PinsHaveSettled()
	{
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;

		gameManager.Bowl(pinFall);

		lastStandingCount = -1; //Indicates pins have settled, and ball not back in box
		text.color = Color.green;
		ballOutOfPlay = false;
	}

	public void SetBallOutOfPlay()
	{
		ballOutOfPlay = true;
	}

	public void Reset()
	{
		lastSettledCount = 10;
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.name == "BowlingBall")
		{
			ballOutOfPlay = true;
		}
	}
}
