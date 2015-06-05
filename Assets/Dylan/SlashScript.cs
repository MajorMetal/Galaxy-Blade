using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlashScript : MonoBehaviour {
	
	private enum _SwipeState{None, Start, Swiping, End};
	public Animator playerAnim;
	public static bool swipeRight;
	public static bool swipeLeft;
	public static bool swipeUp;
	public static bool swipeDown;
	
	
	
	
	
	
	
	
	
	
	void Start () {
		playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("return")) { playerAnim.SetBool("isStrikingLeft", true); }
		if (Input.GetKeyUp("return")) { playerAnim.SetBool("isStrikingLeft", false); }
	}

	void OnEnable(){
		//subscribe to an event
		IT_Gesture.onSwipeStartE += OnSwipe; 
		}
		
	void OnDisable(){ //unsubscribe to an event
		IT_Gesture.onSwipeEndE -=  OnSwipe;
		;
		 }
		 
	
	//function call when event is fired
	
	void SlashLeft() {
		swipeLeft = playerAnim.GetBool("isStrikingLeft");
		playerAnim.SetBool("isStrikingLeft", false);
		Debug.Log("SlashLeft False");
		
		}
		
	void SlashUp() {
		swipeUp = playerAnim.GetBool("isStrikingUp");
		playerAnim.SetBool("isStrikingUp", false);
		Debug.Log("SlashUP False");
	
	}
	
	void SlashRight() {
		swipeRight = playerAnim.GetBool("isStrikingRight");
		playerAnim.SetBool("isStrikingRight", false);
		Debug.Log("SlashRight False");
		
	}
	
	void SlashDown() {
		swipeUp = playerAnim.GetBool("isStrikingDown");
		playerAnim.SetBool("isStrikingDown", false);
		Debug.Log("SlasDownP False");
		
	}
	
	
	//detect swipe direction
	void OnSwipe(SwipeInfo sw)
	{
		if(sw.angle>=45 && sw.angle<135)
		{			
			swipeUp = true;
			playerAnim.SetBool("isStrikingDown", true);
			Invoke("SlashDown", .9f);
			Debug.Log("swipe down");
	
		}
		
		else if(sw.angle>=135 && sw.angle<225)
		{
			swipeLeft = true;
			playerAnim.SetBool("isStrikingUp", true);			
			Invoke("SlashUp", .9f);
			Debug.Log("swipe up");
		}
		
		else if(sw.angle>=225 && sw.angle<315)
		{
			swipeLeft = true;
			playerAnim.SetBool("isStrikingLeft", true);			
			Invoke("SlashLeft", .9f);
			Debug.Log("swipe left");
		}
		
		else{
			swipeLeft = true;
			playerAnim.SetBool("isStrikingRight", true);			
			Invoke("SlashRight", .9f);
			Debug.Log("swipe right");
		}
	}
	
	
}

