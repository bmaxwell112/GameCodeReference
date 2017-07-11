using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private float startTime, endTime;
	private Vector3 dragStart, dragEnd;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}
	
    public void DragStart()
    {
        // Capture time and position of drag start
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd()
    {
        // DragLaunch the ball
        dragEnd = Input.mousePosition;
        endTime = Time.time;

		float dragDuration = endTime - startTime;
		float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
		float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

		Vector3 launchVelocity = new Vector3 (launchSpeedX, 0, launchSpeedZ);
		if (!ball.inPlay) {
			ball.Launch (launchVelocity);
		}
    }

	public void MoveStart(float xNudge){
		if (!ball.inPlay) {
			float xPos = Mathf.Clamp(ball.transform.position.x + xNudge, -50f, 50f);
			float zPos = ball.transform.position.z;
			float yPos = ball.transform.position.y;
			ball.transform.position = new Vector3(xPos, yPos, zPos);
		}
	}
}
