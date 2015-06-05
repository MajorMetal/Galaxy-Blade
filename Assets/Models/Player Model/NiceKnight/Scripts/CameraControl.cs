using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	private float angle = 80;
	private float radius = 4.0f;
	private float minHeight = 0.5f;
	private float maxHeight = 2.0f;
	private float targetCenter = 1.0f;
	
	private float currentRadius = 5.0f; 
	private float verticalPosition;
	
	private float calRadius;
	
	private Vector2 angleSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButton(0)){
	
			angleSpeed.x -= Input.GetAxis("Mouse X") * 80.0f;
			angleSpeed.y -= Input.GetAxis("Mouse Y") * 80.0f;
	
		}
		
		angleSpeed.x = Mathf.Lerp(angleSpeed.x, 0, Time.deltaTime * 5.0f);
		angleSpeed.y = Mathf.Lerp(angleSpeed.y, 0, Time.deltaTime * 5.0f);
		
		angle += angleSpeed.x * Time.deltaTime;		
	
		if (Mathf.Abs(radius - currentRadius) > 0.01f )
		{
			if ( radius < currentRadius )
			{
				radius = radius + Mathf.Abs(radius - currentRadius)*0.05f;
			}
			else
			{
				radius = radius - Mathf.Abs(radius - currentRadius)*0.05f;
			}
		}
		
		verticalPosition = transform.position.y;
		verticalPosition = verticalPosition + angleSpeed.y * 0.02f * Time.deltaTime ;
		verticalPosition = Mathf.Clamp(verticalPosition,minHeight,maxHeight);
		
		calRadius = Mathf.Lerp(radius, radius*0.4f, (verticalPosition-minHeight)/(maxHeight-minHeight) ); 
		
		Vector2 HorizontalPosition ;
		HorizontalPosition.x = Mathf.Cos(angle * Mathf.Deg2Rad) * calRadius;
		HorizontalPosition.y = Mathf.Sin(angle * Mathf.Deg2Rad) * calRadius;

		transform.position = new Vector3(HorizontalPosition.x, verticalPosition, HorizontalPosition.y);		
		transform.LookAt(new Vector3(0,targetCenter,0));
	}
}
