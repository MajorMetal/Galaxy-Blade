using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySword : MonoBehaviour {

	public GameObject player;
	private Animator playerAnim;
	private PlayerHealth playerHealth;
	public GameObject tutorialManager;
	private Tutorial tutorial;
	public GameObject damageSphere;
	private MeshRenderer damageRend;
	
	public int attackDamage = 10;
	public float dazedTime = 0f;
	public bool hit;
	public bool block;
	public bool parry;
	
	public AudioClip EnemyStrikeR;
	public AudioClip EnemyStrikeL;
	public AudioClip EnemyBlock;
	public AudioClip PlayerGrunt;
	public AudioClip EnemyDie;

	
	void Awake() {
		playerAnim = player.GetComponent<Animator>();
		playerHealth = player.GetComponent<PlayerHealth>();
		tutorial = tutorialManager.GetComponent<Tutorial>();
		damageRend = damageSphere.GetComponent<MeshRenderer>();
		damageRend.enabled = false;
	}
	
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player" && !playerAnim.GetBool("isBlocking")) {
			hit = true;
			block = false;
			damageRend.enabled = true;
			GetComponent<AudioSource>().PlayOneShot( EnemyStrikeL, 1.0F);
			GetComponent<AudioSource>().PlayOneShot( PlayerGrunt, 1.0F);
			Attack();
		}
		if (col.gameObject.tag == "PlayerShield" && playerAnim.GetBool("isBlocking")) {
			block = true;
			hit = false;
			dazedTime = dazedTime + 1f;
			tutorial.turnCounter++;
			GetComponent<AudioSource>().PlayOneShot( EnemyBlock, 1.0F);
		}
		if (col.gameObject.tag == "PlayerSword") { parry = true; }	
	}
	
	
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player") {
			hit = false;
			damageRend.enabled = false;
		}
		if (col.gameObject.tag == "PlayerShield") { block = false; }
		if (col.gameObject.tag == "PlayerSword") { parry = false; }	
	}
	
	
	public void Attack() {
		if (playerHealth.currentHealth > 0) { playerHealth.TakeDamage (attackDamage); }
	}
			
}