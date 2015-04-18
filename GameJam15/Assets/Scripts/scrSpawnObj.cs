using UnityEngine;
using System.Collections;

public class scrSpawnObj : MonoBehaviour {

	public GameObject[] boundary;
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
		int spawnNum = Random.Range (1, 3);
		
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		for (int i = 0; i < spawnNum; i++) {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			int boundaryIndex = Random.Range (0, boundary.Length);
			Instantiate (boundary [boundaryIndex], spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}
}
