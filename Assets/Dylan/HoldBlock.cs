using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 

{
public Animator anim;
public GameObject button;
public bool pointerDown = false;
//public Text text;
	




	// Use this for initialization
	public void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("isBlocking", false);
		
	
	}
	
	public void Update (){
		if (Input.GetKeyDown("space")) { anim.SetBool("isBlocking", true); }
		if (Input.GetKeyUp("space")) { anim.SetBool("isBlocking", false); }
	}
	
	
	public void OnPointerDown(PointerEventData eventData) {
		pointerDown = true;
		//Debug.Log("START BLOCKING");
		anim.SetBool("isBlocking", true);
		//Debug.Log ("BlockStarted");
	}
	
	public void OnPointerUp(PointerEventData eventData) {
	
		pointerDown = false;
		//Debug.Log("STOP BLOCKING!");
		anim.SetBool("isBlocking", false);
		
	}
	
	public void OnPointerEnter(PointerEventData eventData) {
		
	//	Debug.Log("Button Color Change");
	
		
//		Text t = text.GetComponent<Text>(); 
//		t.colors.normalColor = Color.red;
//		ColorBlock cb = t.colors;
//		cb.normalColor = Color.white;
//		t.colors = cb;
		
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		
		Debug.Log("Button Color Change Back");
	}
//	public void onClick(){
//		
//		anim.SetBool("isBlocking", true);
//		//button.SetActive(false);
		
		
			
	}
	