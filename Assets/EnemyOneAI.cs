using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyOneAI : MonoBehaviour {
	
	public GameObject enemy;
	private EnemyHealth enemyHealth;
	public GameObject enemySword;
	private EnemySword daze;
	public GameObject tutorialManager;
	private Tutorial tutorial;
	public Animator enemyAnim;
	public int hitCounter = 0;
	private int countCheck;
	private float? timer = null;
	
	
	void Awake () {
		enemyHealth = enemy.GetComponent<EnemyHealth>();
		daze = enemySword.GetComponent<EnemySword>();
		tutorial = tutorialManager.GetComponent<Tutorial>();
	}
	
	
	void Update () {
		if (hitCounter > 0 && !enemyAnim.GetBool ("isDazed") && !enemyAnim.GetBool ("clickToStart") && !timer.HasValue) {
			countCheck = hitCounter;
			timer = Time.time + 3f;
			enemyAnim.SetTrigger ("Block");
		}
		
		if (hitCounter > 0 && timer.HasValue && (Time.time > timer.Value)) {
			Debug.Log ("Made it here?");
			hitCounter = 0;
			EnemyAttackPattern ();
		}
		
		
		if (tutorial.turnCounter > 3 && !enemyAnim.GetBool ("isDazed")) {
			enemyAnim.SetBool ("isDazed", true);
			timer = Time.time + daze.dazedTime;
			tutorial.turnCounter = 1;
		}
		
		if (Time.time > timer.Value && enemyAnim.GetBool ("isDazed")) {
			Debug.Log ("Why you no make it D:");
			timer = null;
			daze.dazedTime = 0f;
			enemyAnim.SetBool ("isDazed", false);
			tutorial.phaseCounter++;
			EnemyAttackPattern ();
		}
	}
	
	
	void EnemyAttackPattern () {
		if (enemyHealth.currentHealth < (enemyHealth.startingHealth / 2)) {
			enemyAnim.SetTrigger ("AttackPattern2");
		}
		else {
			enemyAnim.SetTrigger ("AttackPattern1");
		}
	}
	
}
