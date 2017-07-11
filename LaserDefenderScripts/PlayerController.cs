using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject laserPrefab;
	
	public float speed = 15f;
	public float padding = 1f;
	public float laserSpeed;
	public float firingRate = 2f;
	public float health =200f;
	public GameObject smoke;
	
	public AudioClip hitSound;
	public AudioClip fireSound;
	
	public float xmin;
	public float xmax;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		Destroy (gameObject);
		man.LoadLevel("End Menu");
		
	}
	
	void Fire(){
		GameObject beam = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,laserSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;		
		}
		
		
		//restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.getDamage();
			missile.Hit();
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
			if (health <= 0){
				Die();
			} else {
				Vector3 offset = new Vector3(0, -0.5f, 0);
				GameObject smokePuff = Instantiate(smoke, transform.position+offset, Quaternion.identity) as GameObject;
				smokePuff.transform.parent = this.transform;			
			}
		}
	}
}
