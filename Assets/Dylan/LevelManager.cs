using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	float? m_timer = null;
	
	public void LoadLevel(string name){
		
		Application.LoadLevel("Game");
		
		//Debug.Log ("New Level load: " + name);
		//Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
