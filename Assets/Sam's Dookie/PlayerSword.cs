using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSword : MonoBehaviour {

	public GameObject enemyOne;
	public GameObject enemyTwo;
	private Animator enemyOneAnim;
	private Animator enemyTwoAnim;
	private EnemyAI enemyOneAI;
	private EnemyAI enemyTwoAI;
	private EnemyHealth enemyOneHealth;
	private EnemyHealth enemyTwoHealth;
	public GameObject tutorialManager;
	private Tutorial tutorial;
	private ScoreKeeper scoreKeeper;
	public int attackDamage = 10;
	public int scoreValue = 100;
	public bool hit;
	public bool block;
	public bool parry;
	private float? timer = null;
	
	public AudioClip PlayerStrikeR;
	public AudioClip PlayerStrikeL;
	public AudioClip PlayerBlock;
	public AudioClip EnemyGrunt;
	public AudioClip PlayerDie;
	
	
	void Awake() {
		enemyOneAnim = enemyOne.GetComponent<Animator>();
		enemyOneAI = enemyOne.GetComponent<EnemyAI>();
		enemyOneHealth = enemyOne.GetComponent<EnemyHealth>();
		enemyTwoAnim = enemyTwo.GetComponent<Animator>();
		enemyTwoAI = enemyTwo.GetComponent<EnemyAI>();
		enemyTwoHealth = enemyTwo.GetComponent<EnemyHealth>();
		tutorial = tutorialManager.GetComponent<Tutorial>();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	
	void Update () {
		if (timer.HasValue && Time.time > timer.Value) { timer = null; }
	}
	
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Enemy" && !timer.HasValue && (enemyOneAnim.GetBool("isDazed") ||
																 enemyTwoAnim.GetBool("isDazed"))) {
			timer = Time.time + .9f;
			hit = true;
			Attack();
			GetComponent<AudioSource>().PlayOneShot( PlayerStrikeL, 1.0F);
			GetComponent<AudioSource>().PlayOneShot( EnemyGrunt, 1.0F);
		}
		else if (col.gameObject.tag == "Enemy" && !timer.HasValue && (!enemyOneAnim.GetBool("isBlocking") ||
		                                                              !enemyTwoAnim.GetBool("isBlocking"))) {
			timer = Time.time + .9f;
			hit = true;
			block = false;
			Attack();
			GetComponent<AudioSource>().PlayOneShot( PlayerStrikeL, 1.0F);
			GetComponent<AudioSource>().PlayOneShot( EnemyGrunt, 1.0F);
		}
		else if (col.gameObject.tag == "EnemyShield" && (enemyOneAnim.GetBool("isBlocking") ||
														 enemyTwoAnim.GetBool("isBlocking"))) {
			block = true;
			hit = false;
			GetComponent<AudioSource>().PlayOneShot( PlayerBlock, 1.0F);
		}
		else if (col.gameObject.tag == "EnemySword") { parry = true; }	
	}


	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Enemy") { hit = false; }
		if (col.gameObject.tag == "EnemyShield") { block = false; }
		if (col.gameObject.tag == "EnemySword") { parry = false; }	
	}
	
	
	public void Attack() {
		if (enemyOneHealth.currentHealth > 0) {
			enemyOneHealth.TakeDamage (attackDamage);
			if (!enemyOneAnim.GetBool ("clickToStart")) { enemyOneAI.hitCounter++; }
		}
		else if (enemyTwoHealth.currentHealth > 0) {
			enemyTwoHealth.TakeDamage (attackDamage);
			if (!enemyTwoAnim.GetBool ("clickToStart")) { enemyTwoAI.hitCounter++; }
		}
		
		if (enemyOneAnim.GetBool ("isDazed") || enemyTwoAnim.GetBool ("isDazed")) {
			scoreKeeper.Score(scoreValue);
		}
	}
	
}
