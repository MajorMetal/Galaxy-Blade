using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public GameObject enemy;
	public GameObject bossOne;
	public GameObject bossTwo;
	public Animator enemyAnim;
	public GameObject tutorialManager;
	private Tutorial tutorial;
	public int startingHealth = 300;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider enemyHealthSlider;
	public Slider playerHealthSlider;
	public static bool isDead;
	private float? timer = null;
	
	
	
	void Awake () {
		currentHealth = startingHealth;
		tutorial = tutorialManager.GetComponent<Tutorial>();
		// enemyTwo.SetActive(false);
	}
	
	
	void Update () {
		if (timer.HasValue && Time.time > timer.Value) {
			timer = null;
			Debug.Log ("Make it here?");
			Death ();
		}
	}
	
	
	public void TakeDamage (int amount) {
		if (enemyAnim.GetBool ("isDazed")) { currentHealth -= amount; }
		else if (!enemyAnim.GetBool ("isBlocking")) { currentHealth -= (amount / 2); }
		
		if (currentHealth <= 0f && !timer.HasValue) {
			timer = Time.time + 5f;
			
			if (!enemyAnim.GetBool ("isDazed")) { enemyAnim.SetBool ("isDazed", true); }
		}
		
		enemyHealthSlider.value = currentHealth;
		Debug.Log ("Enemy has " + currentHealth + " left.");
	}
	
	
	void Death () {
		isDead = true;
		enemyAnim.SetBool ("isDazed", false);
		enemyAnim.SetTrigger ("Death");
		Invoke("GameOver", 5f);
		Debug.Log ("You Win!");
	}
	
	
	void GameOver () {
		if (tutorial.bossCounter == 1) {
			enemyHealthSlider.value = startingHealth;
			playerHealthSlider.value = startingHealth;
			tutorial.bossCounter++;
			tutorial.engageButton.SetActive(true);
			bossOne.SetActive(false);
			bossTwo.SetActive(true);
		}
		else if (tutorial.bossCounter == 2) {
			Application.LoadLevel("WinScreen");
		}
	}
	
}
