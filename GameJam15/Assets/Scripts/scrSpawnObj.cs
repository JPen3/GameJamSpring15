using UnityEngine;
using System.Collections;

public class scrSpawnObj : MonoBehaviour {

	public GameObject boundary;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn ()
	{
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (boundary, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
