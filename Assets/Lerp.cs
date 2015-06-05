using UnityEngine;
using System.Collections;

public class Lerp : MonoBehaviour {

public Transform target01;
public Transform target02;
public float fract;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = Vector3.Lerp (target01.position, target02.position, fract);
	}
}
