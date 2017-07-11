using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {

	[Tooltip ("Average number of seconds between appearances")]
	public float seenEverySeconds;
	private Animator anim;
	private float currentSpeed;
	private GameObject currentTarget;
	
	// Use this for initialization
	void Start () {
		Rigidbody2D myRidgidBody = gameObject.AddComponent<Rigidbody2D>();
		myRidgidBody.isKinematic = true;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * currentSpeed * Time.deltaTime);
		if(!currentTarget){
			anim.SetBool ("isAttacking", false);
		}
			
	}
	
	void OnTriggerEnter2D (Collider2D collider)
    {
        GameObject obj = collider.gameObject;
        
        if (!obj.GetComponent<Projectile>())
        {
            return;
        }
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        Invoke("ChangeColor", 0.25f);
    }

    void ChangeColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

	public void SetSpeed (float speed){
		currentSpeed = speed;
	}
	// called from the animator at the time of the attack
	public void StrikeCurrentTarget (float damage){
		if(currentTarget){
			Health health = currentTarget.GetComponent<Health>();
			if (health){
				health.dealDamage(damage);
			}
		}
		
	}
	
	public void Attack (GameObject obj){
		currentTarget = obj;
		
	}
}
