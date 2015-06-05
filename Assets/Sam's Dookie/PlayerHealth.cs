using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public Animator playerAnim;
	public GameObject damageSphere;
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider; 
	public static bool isDead;


	void Awake() {
		currentHealth = startingHealth;
	}


	public void TakeDamage (int amount) {
		currentHealth -= amount;
		
		if (currentHealth <= 0f) {
			//isDEAD TRUE 
			isDead = true;
			playerAnim.SetBool ("isDead", true);
			Invoke("GameOver", 5f);
			Debug.Log ("You Lost!");
		}
		
		healthSlider.value = currentHealth;
		Debug.Log ("Player has " + currentHealth + " left.");
	}
	
	
	 void GameOver () { Application.LoadLevel("LoseScreen"); }
			
}
	