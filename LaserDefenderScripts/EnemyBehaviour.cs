using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject enemyLaserPrefab;
	public float projectileSpeed = 10f;
	public float health = 150f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip laserFire;
	public AudioClip deathSound;
	public AudioClip hitSound;
	public GameObject smoke;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();	
	}

	void Update() {
		float probabitlity = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probabitlity){
			Fire ();
		}		
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log("Hit by missile");
			health -= missile.getDamage();
			missile.Hit();
			if (health <= 0){
				Die();			
			} else {
				AudioSource.PlayClipAtPoint(hitSound, transform.position);
				Vector3 offset = new Vector3(0, -0.5f, 0);
				GameObject smokePuff = Instantiate(smoke, transform.position+offset, Quaternion.identity) as GameObject;
				smokePuff.transform.parent = this.transform;
			}
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy (gameObject);
		difficulty();
		scoreKeeper.Score(scoreValue);	
	}
	
	void Fire(){
		GameObject missile = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(laserFire, transform.position);
	}
	
	public bool IsDivisble(int x)
	{
		return (x % 1500) == 0;
	}
	
	void difficulty() {
		if(IsDivisble(ScoreKeeper.score)){
			shotsPerSeconds += 0.5f;
		}	
	}
}
