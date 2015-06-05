using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PSGuiEffects : MonoBehaviour {

	
		
		public Text blockedText;
		public Text hitText;
		public float fadeSpeed = 5f;
		public bool hit;
		public bool block;
		public Canvas hitCanvas;
		public Canvas blockCanvas;
		public Animator anim;
		
		
		
		void Awake()
		{
			blockedText = blockCanvas.GetComponentInChildren<Text> ();
			blockedText.color = Color.clear;
			hitText = hitCanvas.GetComponentInChildren<Text> ();
			hitText.color = Color.clear;
		}
		
		void Update()
		{
			ColorChange ();
			
		}
		
		void OnTriggerEnter(Collider col) 
		{
			
			
			if (col.gameObject.tag == "Enemy" && anim.GetBool("isBlocking") == false)
			{
				StartCoroutine("DoTheDance"); 
			}
			if (col.gameObject.tag == "Enemy" && anim.GetBool("isBlocking"))
			{
				StartCoroutine("DoTheBlock"); 
			}
			
		}
		
		public IEnumerator DoTheDance() 
		{
			hit = true;
			yield return new WaitForSeconds(1f); // waits 3 seconds
			hit = false; // will make the update method pick up 
		}
		
		public IEnumerator DoTheBlock() 
		{
			block = true;
			yield return new WaitForSeconds(1f); // waits 3 seconds
			block = false; // will make the update method pick up
		}
		//	void OnTriggerExit(Collider col) {
		//		
		//		if (col.gameObject.tag == "Player")
		//		{
		//			hit = false;
		//			
		//		}
		//		if (col.gameObject.tag == "PlayerShield")
		//		{
		//			block = false;
		//			
		//			
		//		}
		
		//	}
		
		public void ColorChange()
		{
			if (block)
			{
				blockedText.color = Color.Lerp (blockedText.color, Color.red, fadeSpeed * Time.deltaTime);
			}	
			
			if (!block)
			{	
				blockedText.color = Color.Lerp (blockedText.color, Color.clear, fadeSpeed * Time.deltaTime);
			}
			
			if (hit)
			{
				hitText.color = Color.Lerp (hitText.color, Color.blue, fadeSpeed * Time.deltaTime);
				Debug.Log ("Player Hit Enemy");
			}
			if (!hit)
			{
				hitText.color = Color.Lerp (hitText.color, Color.clear, fadeSpeed * Time.deltaTime);
			}
		}
}
