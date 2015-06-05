using UnityEngine;
using System.Collections;

public class rotationTest : MonoBehaviour 

{
	public float rotationSpeed = 180;
	public float siclePauseTime = 0.5f;
	
	void Start ()
	{
		StartCoroutine(RotationInitializer());
	}
	
	IEnumerator RotationInitializer()
	{
		while (true)
		{
			int axis = Random.Range(1, 5);
			
			switch (axis)
			{
			case 1:
				print ("up");
				yield return StartCoroutine(RotateTo(Quaternion.AngleAxis(90 * Mathf.Sign(Random.Range(-1, 2)), Vector3.up)*transform.rotation));
				break;
			case 2:
				print ("right");
				yield return StartCoroutine(RotateTo(Quaternion.AngleAxis(90 * Mathf.Sign(Random.Range(-1, 2)), Vector3.right)*transform.rotation));
				break;
			case 3:
				print ("down");
				yield return StartCoroutine(RotateTo(Quaternion.AngleAxis(90 * Mathf.Sign(Random.Range(-1, 2)), Vector3.down)*transform.rotation));
				break;
			case 4:
				print ("left");
				yield return StartCoroutine(RotateTo(Quaternion.AngleAxis(90 * Mathf.Sign(Random.Range(-1, 2)), Vector3.left)*transform.rotation));
				break;
			}
			
			
			yield return new WaitForSeconds(siclePauseTime);
		}
	}
	
	IEnumerator RotateTo(Quaternion target)
	{
		while (Quaternion.Angle(transform.rotation, target)>0.5f)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed*Time.deltaTime*5);
			yield return null;
		}
		transform.rotation = target;
	}
}