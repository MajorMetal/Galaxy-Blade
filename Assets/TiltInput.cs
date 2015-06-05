using UnityEngine;
using System.Collections;

public class TiltInput : MonoBehaviour {
	
	public GameObject player;
	public GameObject tiltController;
	public Animator anim;
	public bool tiltLeft;
	public bool tiltRight;
	private Transform tiltPos;
	
	
	
	
	
	
	
	
	void Awake()
	{
		tiltPos = tiltController.GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator>();
			
	}
	
	void Update()
	{
	
	if (tiltPos.rotation.z <= -.10f)
		{
			tiltLeft = true;
			DodgeLeft();
			
		}
		
		
	if (tiltPos.rotation.z >= .10f)
		{
		    tiltRight = true;
			DodgeRight();
			
		}	
	
	}
	
	
	
	void DodgeLeft()
	
		
	{
		
		anim.SetBool("isDodgingLeft",true );
		Debug.Log("DodgeLeft");
		tiltLeft = false;
		
		 
	}
			
	 
	 
	void DodgeRight()
	{
		
		
		;
		anim.SetBool("isDodgingRight", true);
		Debug.Log ("DodgeRight");
		tiltRight = false;
		
	
	
		
	}
	
	public IEnumerator BacktoRight() 
	{
		
		yield return new WaitForSeconds(1f); // waits 3 seconds
		tiltLeft = false; // will make the update method pick up
	}	
	
	public IEnumerator BacktoLeft() 
	{
		
		yield return new WaitForSeconds(1f); // waits 3 seconds
		tiltRight = false; // will make the update method pick up
	}	
	
}
