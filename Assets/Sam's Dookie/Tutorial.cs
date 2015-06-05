using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
	public GameObject player;
	public GameObject enemyOne;
	public GameObject enemyTwo;
	public GameObject engageButton;
	public Canvas startFightCanvas;
	public Canvas blockCanvas;
	public Canvas strikeHorizontalCanvas;
	public Canvas strikeVerticalCanvas;
	
	public int turnCounter = 1;
	public int phaseCounter = 1;
	public int bossCounter = 1;
	private float? timer = null;
	private float? wait = null;
	
	readonly int k_isBlocking = Animator.StringToHash("isBlocking");
	readonly int k_isStrikingRight = Animator.StringToHash("isStrikingRight");
	readonly int k_isStrikingLeft = Animator.StringToHash("isStrikingLeft");
	readonly int k_isStrikingUp = Animator.StringToHash("isStrikingUp");
	readonly int k_isStrikingDown = Animator.StringToHash("isStrikingDown");
	readonly int k_isDodgingRight = Animator.StringToHash("isDodgingRight");
	readonly int k_isDodgingLeft = Animator.StringToHash("isDodgingLeft");
	
	Animator playerAnim;
	Animator enemyOneAnim;
	Animator enemyTwoAnim;
	
	enum AnimStateCheckType {
		WaitForStrike,
		Strike,
		Block,
		Idle
	}
	
	AnimStateCheckType checkState = AnimStateCheckType.Idle;
	
	
	void Awake () {
		enemyTwo.SetActive(false);
		playerAnim = player.GetComponent<Animator>();
		enemyOneAnim = enemyOne.GetComponent<Animator>();
		enemyTwoAnim = enemyTwo.GetComponent<Animator>();
		blockCanvas.enabled = false;
		strikeHorizontalCanvas.enabled = false;
		strikeVerticalCanvas.enabled = false;
	}
	
	
	public void onClick () {
		if (bossCounter == 1) { enemyOneAnim.SetBool ("clickToStart", true); }
		else if (bossCounter == 2) { enemyTwoAnim.SetBool ("clickToStart", true); }
		engageButton.SetActive(false);
		timer = Time.time + 4f;
	}
	
	
	void Update () {
		if (timer.HasValue && Time.time > timer.Value) {
			timer = null;
			
			if (bossCounter == 1) {
				enemyOneAnim.SetBool ("clickToStart", false);
				enemyOneAnim.SetTrigger ("AttackPattern1");
				waitForBlock();
			}
			else if (bossCounter == 2) {
				enemyTwoAnim.SetBool ("clickToStart", false);
				enemyTwoAnim.SetTrigger ("AttackPattern1");
			}
		}
		
		switch (checkState) {
			
		case AnimStateCheckType.WaitForStrike:
			if (wait.HasValue && Time.time > wait.Value) {
				wait = null;
				//waitForStrike();
			}
			break;
			
		case AnimStateCheckType.Strike:
			if (playerAnim.GetBool(k_isStrikingRight) || playerAnim.GetBool(k_isStrikingLeft) ||
			    playerAnim.GetBool(k_isStrikingUp) || playerAnim.GetBool(k_isStrikingDown)) {
				
				if (strikeHorizontalCanvas.enabled == true && (playerAnim.GetBool(k_isStrikingRight) ||
				                                               playerAnim.GetBool(k_isStrikingLeft))) {
					Time.timeScale = 1;
					strikeHorizontalCanvas.enabled = false;
				}
				else if (strikeVerticalCanvas.enabled == true && (playerAnim.GetBool(k_isStrikingUp) ||
				                                                  playerAnim.GetBool(k_isStrikingDown))) {
					Time.timeScale = 1;
					strikeVerticalCanvas.enabled = false;
				}
				else if (bossCounter == 1 && phaseCounter == 1 && turnCounter == 2) {
					checkState = AnimStateCheckType.Idle;
				}
				else if (turnCounter > 3) {
					if (bossCounter == 1) {
						phaseCounter++;
						turnCounter = 1;
						checkState = AnimStateCheckType.Idle;
					}
				}
			}
			break;
			
		case AnimStateCheckType.Block:
			if (blockCanvas.enabled == true) {
				Time.timeScale = 1;
				blockCanvas.enabled = false;
			}
			
			if (turnCounter > 3) {
				if (bossCounter == 1 && phaseCounter == 1) {
					turnCounter = 1;
					wait = Time.time + 2f;
					checkState = AnimStateCheckType.WaitForStrike;
				}
				else {
					turnCounter = 1;
					checkState = AnimStateCheckType.Strike;
				}
			}
			break;
			
		case AnimStateCheckType.Idle:
			if (playerAnim.GetBool(k_isBlocking)) { checkState = AnimStateCheckType.Block; }
			break;
			
		default:
			break;
			
		}
		
	}
	
	
	void waitForBlock () {
		Time.timeScale = 0;
		Debug.Log ("GUI Frozen until Block");
		checkState = AnimStateCheckType.Idle;
		blockCanvas.enabled = true;
	}
	
	
//	void waitForStrike () {
//		Time.timeScale = 0;
//		Debug.Log("GUI Frozen until Strike");
//		checkState = AnimStateCheckType.Strike;
//		
//		if (turnCounter == 1) { strikeHorizontalCanvas.enabled = true; }
//		else if (turnCounter == 2) { strikeVerticalCanvas.enabled = true; }
//	}
	
}