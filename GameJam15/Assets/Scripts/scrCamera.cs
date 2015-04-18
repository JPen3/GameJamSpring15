using UnityEngine;
using System.Collections;

//Attach to camera to apply dampening
public class scrCamera : MonoBehaviour 
{
	public GameObject target; //Player Object

	private Vector3 offset; //Camera offset
	private Vector3 newPosition; //Position to damp to
	private float smoothTime = 0.2F; //Damp time

	private Vector3 velocity = Vector3.zero;

	//Object initialization
	void Start () 
	{
		//Get offset of camera from player
		offset = transform.position;
	}

	//
	void LateUpdate () 
	{
		//(Start position, Target position, Velocity, Speed of dampening)
		newPosition = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref velocity, smoothTime);

		//Set new position of camera
		transform.position = newPosition;
	}
}