﻿using UnityEngine;
using System.Collections;

public class scrBoundaryMove : MonoBehaviour {
	public float speed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
		/*if (!GetComponent<Renderer>().isVisible) {
			Debug.Log ("It's gone Jim.");
			Destroy(gameObject);
		}*/

		if(transform.position.z <= -5.0F)
		{
			Destroy(gameObject);
		}
	}

	/*void OnBecameInvisible() {
		Debug.Log ("It's gone Jim.");
		Destroy (gameObject);
	}*/
}
