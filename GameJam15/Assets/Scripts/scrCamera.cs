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

	private bool battleCamera;

	//FOR iTWEEN MOVEMENT
	private Vector3[] path;
	private Vector3 rootPosition;
	private Vector3 targetOffset = new Vector3(-6.3F, -1.71F, 2.71F);
	private Vector3 targetPath;

	//Object initialization
	void Start () 
	{
		battleCamera = false;
		//Get offset of camera from player
		offset = transform.position;
	}

	//Late update
	void LateUpdate () 
	{
		if(!battleCamera)
		{
			//(Start position, Target position, Velocity, Speed of dampening)
			newPosition = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref velocity, smoothTime);

			//Set new position of camera
			transform.position = newPosition;
		}

		transform.LookAt(GameObject.Find("Pivot").transform);
	}

	public void StartBattleCamera()
	{
		battleCamera = true;

		GeneratePath();
		iTween.MoveTo(gameObject, iTween.Hash("position", targetPath, "time", 3, "easetype", iTween.EaseType.easeOutCirc));
	}

	public void EndBattleCamera(bool won)
	{
		battleCamera = false;

		if(won)
		{
			GeneratePath();
			iTween.MoveTo(gameObject, iTween.Hash("position", targetPath, "time", 3, "easetype", iTween.EaseType.easeOutCirc));
		}
	}

	void GeneratePath()
	{
		rootPosition = transform.position;
		targetPath = new Vector3(rootPosition.x + targetOffset.x,
		                         rootPosition.y + targetOffset.y,
		                         rootPosition.z + targetOffset.z);
		           //path = new Vector3[2];
		//rootPosition = transform.position;
		//path[0] = rootPosition;
		//path[1] = new Vector3(rootPosition.x + targetOffset.x,
		                   //rootPosition.y + targetOffset.y,
		                   //rootPosition.z + targetOffset.z);
		//path[0] = rootPosition;
		//path[1] = new Vector3(rootPosition.x + targetOffset;
	}
}