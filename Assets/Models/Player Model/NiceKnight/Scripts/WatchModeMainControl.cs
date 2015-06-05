using UnityEngine;
using System.Collections;

public class WatchModeMainControl : MonoBehaviour {

	//Ui Camera
	public Camera uiCamera;

	// Character Costume
	public CharacterCostume _characterCostume;

	// Weapon
	public Weapons _weapons;

	//Animator Contoller
	public RuntimeAnimatorController AnimController;

	private string clickedButton;
	private int currentCharacter;
	private int currentAnimation;
	private Transform[] currentWeapon;
	private Transform[] nextWeapon;

	private Animator Anim;
	private Transform[,] weaponPoint;
	private string[] actionButton;
	private string[] animationList;
	
	// Use this for initialization
	void Start () {

		float currentTime = Time.realtimeSinceStartup;

		weaponPoint = new Transform[2,3]; // {{RightHand,leftHand}{Costume0,Costume1,Costume2}}
		currentWeapon = new Transform[2]; //0 is RightHand, 1 is leftHand
		nextWeapon = new Transform[2]; //0 is RightHand, 1 is leftHand

		actionButton = new string[]
			{"ui_btn_idle", "ui_btn_run", "ui_btn_backwalk", "ui_btn_attack01", "ui_btn_attack02", "ui_btn_attack03", "ui_btn_jump"
			,"ui_btn_statechange","ui_btn_dead", "ui_btn_idle_n", "ui_btn_run_n", "ui_btn_backwalk_n","ui_btn_statechange_n"};

		animationList = new string[]
			{"Base Layer.IDLE", "Base Layer.RUN", "Base Layer.WALK_BACK", "Base Layer.ATTACK01", "Base Layer.ATTACK02", "Base Layer.ATTACK03", "Base Layer.JUMP"
			,"Base Layer.CHANGE","Base Layer.DEAD", "Base Layer.IDLE_N", "Base Layer.RUN_N", "Base Layer.WALK_BACK_N","Base Layer.CHANGE_N"};
		


		//Initialize Character Costume0
		if (_characterCostume.niceKnightL0){
			_characterCostume.niceKnightL0.position = Vector3.zero;
			_characterCostume.niceKnightL0.GetComponent<Animator>().runtimeAnimatorController = AnimController;

			//Find weapon point
			Transform[] allChilds = _characterCostume.niceKnightL0.GetComponentsInChildren<Transform>(); 
			foreach ( Transform item in allChilds) 
			{ 
				if (item.name == "Bip001 Weapon R" ) 
					weaponPoint[0,0] = item;
				//childs.Add(item); 
				if (item.name == "Bip001 Weapon L" ) 
					weaponPoint[1,0] = item;
			}

			_characterCostume.niceKnightL0.gameObject.SetActive(false);
		}
		else Debug.Log("niceKnightL0 not found!");

		//Initialize Character Costume1
		if (_characterCostume.niceKnightL1){
			_characterCostume.niceKnightL1.position = Vector3.zero;
			_characterCostume.niceKnightL1.gameObject.SetActive(true);
			Anim = _characterCostume.niceKnightL1.GetComponent<Animator>();
			Anim.runtimeAnimatorController = AnimController;

			//Find weapon point
			Transform[] allChilds = _characterCostume.niceKnightL1.GetComponentsInChildren<Transform>(); 
			foreach ( Transform item in allChilds) 
			{ 
				if (item.name == "Bip001 Weapon R" ) 
					weaponPoint[0,1] = item;
				//childs.Add(item); 
				if (item.name == "Bip001 Weapon L" ) 
					weaponPoint[1,1] = item;
			}
		}
		else Debug.Log("niceKnightL1 not found!");

		//Initialize Character Costume2
		if (_characterCostume.niceKnightL2){
			_characterCostume.niceKnightL2.position = Vector3.zero;
			_characterCostume.niceKnightL2.GetComponent<Animator>().runtimeAnimatorController = AnimController;

			//Find weapon point
			Transform[] allChilds = _characterCostume.niceKnightL2.GetComponentsInChildren<Transform>(); 
			foreach ( Transform item in allChilds) 
			{ 
				if (item.name == "Bip001 Weapon R" ) 
					weaponPoint[0,2] = item;
				//childs.Add(item); 
				if (item.name == "Bip001 Weapon L" ) 
					weaponPoint[1,2] = item;
			}

			_characterCostume.niceKnightL2.gameObject.SetActive(false);
		}
		else Debug.Log("niceKnightL2 not found!");

		currentCharacter = 1;
		currentAnimation = Animator.StringToHash("Base Layer.IDLE");

		//Initialize Weapon
		if (_weapons.weaponL0.RightHand){
			_weapons.weaponL0.RightHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL0-RightHand not found!");
		if (_weapons.weaponL0.LeftHand){
			_weapons.weaponL0.LeftHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL0-LeftHand not found!");
		if (_weapons.weaponL1.RightHand){
			_weapons.weaponL1.RightHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL1-RightHand not found!");
		if (_weapons.weaponL1.LeftHand){
			_weapons.weaponL1.LeftHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL1-LeftHand not found!");
		if (_weapons.weaponL2.RightHand){
			_weapons.weaponL2.RightHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL2-RightHand not found!");
		if (_weapons.weaponL2.LeftHand){
			_weapons.weaponL2.LeftHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponL2-LeftHand not found!");
		if (_weapons.weaponX1.RightHand){
			_weapons.weaponX1.RightHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponX1-RightHand not found!");
		if (_weapons.weaponX1.LeftHand){
			_weapons.weaponX1.LeftHand.gameObject.SetActive(false);
		}
		else Debug.Log("weaponX1-LeftHand not found!");

		nextWeapon[0] = _weapons.weaponL1.RightHand;
		nextWeapon[1] = _weapons.weaponL1.LeftHand;
		changeWeapon();
	}
	
	// Update is called once per frame
	void Update () {

		//Ui Size update
		uiCamera.orthographicSize = (float)Screen.height/(float)Screen.width * 1.6f;
		if (uiCamera.orthographicSize<1) uiCamera.orthographicSize = 1;

		if (Input.GetMouseButtonUp(0)) {

			float currentTime = Time.realtimeSinceStartup;

			Ray ray = uiCamera.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
			RaycastHit hit ;

			if(Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				clickedButton = hit.transform.gameObject.name.ToString();
			}		

			//If back to IDLE animation than currentAnimation is IDLE
			if (Anim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash(animationList[0]))
				currentAnimation = Animator.StringToHash(animationList[0]);
			//If back to IDLE_N animation than currentAnimation is IDLE_N
			if (Anim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash(animationList[9]))
				currentAnimation = Animator.StringToHash(animationList[9]);

			//Select Animation
			for (int i = 0; i<actionButton.Length ; i++){
				if (clickedButton == actionButton[i]) {
					currentAnimation = Animator.StringToHash(animationList[i]);
					clickedButton="";
					break;
				}
			}

			//Change Character Costume
			if (clickedButton == "ui_btn_basebody" && currentCharacter != 0) {
				_characterCostume.niceKnightL0.gameObject.SetActive(true);
				_characterCostume.niceKnightL1.gameObject.SetActive(false);
				_characterCostume.niceKnightL2.gameObject.SetActive(false);
				Anim = _characterCostume.niceKnightL0.GetComponent<Animator>();
				currentCharacter = 0;
			}
			if (clickedButton == "ui_btn_beginner_c" && currentCharacter != 1) {
				_characterCostume.niceKnightL0.gameObject.SetActive(false);
				_characterCostume.niceKnightL1.gameObject.SetActive(true);
				_characterCostume.niceKnightL2.gameObject.SetActive(false);
				Anim = _characterCostume.niceKnightL1.GetComponent<Animator>();
				currentCharacter = 1;
			}
			if (clickedButton == "ui_btn_advance_c" && currentCharacter != 2) {
				_characterCostume.niceKnightL0.gameObject.SetActive(false);
				_characterCostume.niceKnightL1.gameObject.SetActive(false);
				_characterCostume.niceKnightL2.gameObject.SetActive(true);
				Anim = _characterCostume.niceKnightL2.GetComponent<Animator>();
				currentCharacter = 2;
			}

			//Change Weapon
			if (clickedButton == "ui_btn_junkset" ) {
				nextWeapon[0] = _weapons.weaponL0.RightHand;
				nextWeapon[1] = _weapons.weaponL0.LeftHand;
			}else if(clickedButton == "ui_btn_beginner_w" ) {
				nextWeapon[0] = _weapons.weaponL1.RightHand;
				nextWeapon[1] = _weapons.weaponL1.LeftHand;
			}else if(clickedButton == "ui_btn_advance_w" ) {
				nextWeapon[0] = _weapons.weaponL2.RightHand;
				nextWeapon[1] = _weapons.weaponL2.LeftHand;
			}else if(clickedButton == "ui_btn_farmer" ) {
				nextWeapon[0] = _weapons.weaponX1.RightHand;
				nextWeapon[1] = _weapons.weaponX1.LeftHand;
			}			
			
			changeWeapon();

			//Play Animation
			Anim.Play(currentAnimation);

			//If ACTION is NORMAL STATE than weapon is invisible
			if (currentAnimation == Animator.StringToHash(animationList[9]) || currentAnimation == Animator.StringToHash(animationList[10])
			    || currentAnimation == Animator.StringToHash(animationList[11]) || currentAnimation == Animator.StringToHash(animationList[12])) {
				currentWeapon[0].gameObject.SetActive(false);
				currentWeapon[1].gameObject.SetActive(false);
			}
			else {
				currentWeapon[0].gameObject.SetActive(true);
				currentWeapon[1].gameObject.SetActive(true);
			}
			
			//When STATE CHANGE animation, Weapon visible
			if (currentAnimation == Animator.StringToHash(animationList[7])) StartCoroutine (toggleWeapon( false, 0.4f));
			else if (currentAnimation == Animator.StringToHash(animationList[12])) StartCoroutine (toggleWeapon( true, 0.4f));

		}
		
	}

	IEnumerator toggleWeapon(bool visible, float time){
		yield return new WaitForSeconds(time);
		currentWeapon[0].gameObject.SetActive(visible);
		currentWeapon[1].gameObject.SetActive(visible);
	}

	void changeWeapon() {
		for (int i = 0; i<2;i++) {
			if (currentWeapon[i]) currentWeapon[i].gameObject.SetActive(false);
			nextWeapon[i].position = weaponPoint[i,currentCharacter].position;
			nextWeapon[i].parent = weaponPoint[i,currentCharacter];
			nextWeapon[i].localScale = Vector3.one;
			nextWeapon[i].localRotation = Quaternion.Euler( Vector3.zero );
			nextWeapon[i].gameObject.SetActive(true);
			currentWeapon[i] = nextWeapon[i];
		}
	}

	[System.Serializable]
	public class CharacterCostume{
		public Transform niceKnightL0;
		public Transform niceKnightL1;
		public Transform niceKnightL2;
	}

	[System.Serializable]
	public class Weapons{
		[System.Serializable]
		public class Weapon {
			public Transform RightHand;
			public Transform LeftHand;
		}
		public Weapon weaponL0;
		public Weapon weaponL1;
		public Weapon weaponL2;
		public Weapon weaponX1;
	}
}
