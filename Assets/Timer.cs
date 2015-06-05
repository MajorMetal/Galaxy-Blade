using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	float? m_timer = null;
	
	
	public void onClick(){
	
		m_timer = Time.time + 4f;
		Debug.Log ("We waited 4 seconds");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
