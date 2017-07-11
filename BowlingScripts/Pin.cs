using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingThreshhold = 3f;
	public float distanceToRaise = 40f;

	private Rigidbody ridgidBody;

	// Use this for initialization
	void Start () {
		ridgidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// print(name + " "  + IsStanding());
	}

	public bool IsStanding(){
		Vector3 rotationInEuler = transform.rotation.eulerAngles;

		float tiltInX = Mathf.Abs(rotationInEuler.x);
		float tiltInZ = Mathf.Abs(rotationInEuler.z);
		if (tiltInX < standingThreshhold && tiltInZ < standingThreshhold)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void RaiseIfStanding()
	{
		if (IsStanding())
		{
			ridgidBody.useGravity = false;
			transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
		}
	}

	public void Lower()
	{
		if (IsStanding())
		{
			transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
			ridgidBody.rotation = Quaternion.Euler(0, 0, 0);
			ridgidBody.useGravity = true;
		}
	}
}
