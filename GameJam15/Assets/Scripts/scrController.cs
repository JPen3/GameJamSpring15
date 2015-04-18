using UnityEngine;
using System.Collections;

public class scrController : MonoBehaviour 
{
	public bool aboveLevel;
	public GameObject wizard;

	public GameObject feather;
	public GameObject windStream;

	private bool spawnFeathers = false;
	private float featherTimer = 0.0F;
	private float featherTime = 0.0F;

	private bool spawnWindStreams = false;
	private float windTimer = 0.0F;
	private float windTime = 0.0F;

	// Use this for initialization
	void Start () 
	{
		LoadBelowLevel();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(spawnFeathers)
		{
			SpawnFeathers();
		}

		if(spawnWindStreams)
		{
			SpawnWindStreams();
		}
	}

	void LoadBelowLevel()
	{
		wizard.SetActive(false);

		spawnFeathers = true;
		featherTime = Random.Range(1, 3);

		spawnWindStreams = true;
		windTime = Random.Range(1, 3);

		aboveLevel = false;
	}

	void LoadAboveLevel()
	{
		/*
		//Spawn wizard
		GameObject wizardObj = Instantiate(wizard, transform.position, transform.rotation) as GameObject;
		wizardObj.name = "Wizard";
		//Set wizard parent to be Entities
		wizardObj.transform.parent = entitiesParent.transform;
		*/

		wizard.transform.position = new Vector3(0, 5.0F, 25.0F);
		wizard.SetActive(true);

		spawnFeathers = false;

		aboveLevel = true;
	}

	void EndGame()
	{
		Debug.Log("YOU LOSE");
	}

	void SpawnFeathers()
	{
		//Increase feather timer
		featherTimer += Time.deltaTime;

		if(featherTimer >= featherTime)
		{
			Instantiate(feather, new Vector3(Random.Range(-10.0F, 10.0F), Random.Range(-20.0F, -10.0F), 50.0F), Quaternion.identity);
			featherTimer = 0.0F;
			featherTime = Random.Range(1, 3);
		}
	}

	void SpawnWindStreams()
	{
		//Increase feather timer
		windTimer += Time.deltaTime;
		
		if(windTimer >= windTime)
		{
			Instantiate(windStream, new Vector3(Random.Range(-10.0F, 10.0F), Random.Range(-20.0F, -10.0F), Random.Range(5.0F, 40.0F)), Quaternion.identity);
			windTimer = 0.0F;
			windTime = Random.Range(1, 3);
		}
	}

	public void Fall(bool above)
	{
		if(above)
		{
			LoadBelowLevel();
		}
		else
		{
			EndGame();
		}
	}

	public void Rise()
	{
		LoadAboveLevel();
	}
}
