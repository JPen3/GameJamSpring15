using UnityEngine;
using System.Collections;

public class scrFeather : MonoBehaviour 
{
	private float speed = 5.0F;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.back * Time.deltaTime * speed);

		if(transform.position.z <= -5.0F)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("Player"))
		{
			Destroy(gameObject);
		}
	}
}
