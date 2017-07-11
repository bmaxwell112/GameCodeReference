using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Defenders : MonoBehaviour {

    
    public int starCost = 100;
	private StarDisplay starDisplay;
    public GameObject healthBar, fullHealthBar;
    private float startingHealth;
    private Health health;

    void Start(){
		starDisplay = GameObject.FindObjectOfType<StarDisplay>();
        health = GetComponent<Health>();
        startingHealth = health.health;
        fullHealthBar.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;
        if (!obj.GetComponent<Attacker>())
        {
            return;
        }
        SetHealthBar();
    }
	
	public void AddStars (int amount){
		starDisplay.AddStars (amount);
	}
    
    public void SetHealthBar()
    {
        healthBar.transform.localScale = new Vector3(health.health / startingHealth, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // print("collided with " + collider);
        GameObject obj = collider.gameObject;
        if (!obj.GetComponent<Attacker>())
        {
            return;
        }
        fullHealthBar.SetActive(true);
    }
}

