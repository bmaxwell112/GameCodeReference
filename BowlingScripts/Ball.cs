using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    
    public Vector3 launchVelocity;
	public bool inPlay = false;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		rigidBody.rotation = Quaternion.identity;
		startPosition = transform.position;
    }

	public void Launch(Vector3 launchVelocity)
    {
		inPlay = true;

		rigidBody.useGravity = true;
		rigidBody.velocity = launchVelocity;

		audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
        
    }

	public void Reset() {
		inPlay = false;
		transform.position = startPosition;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.useGravity = false;
		rigidBody.rotation = Quaternion.identity;
	}
}
