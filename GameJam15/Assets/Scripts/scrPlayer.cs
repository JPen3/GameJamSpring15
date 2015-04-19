using UnityEngine;
using System.Collections;

public class scrPlayer : MonoBehaviour 
{
	public scrController controller;

	private bool playerBattling = false;
	public int counterAttack = 0;

	public bool damaged = false;
	public bool rising = false;

	private int feathers = 0;

	public int health = 3;
	private int flashCounter = 0;

	// Use this for initialization
	void Start() 
	{

	}
	
	// Update is called once per frame
	void Update() 
	{
		if(playerBattling)
		{
			CheckInput();
		}

		if(damaged)
		{
			if(transform.position.y == -15.0F)
			{
				damaged = false;
			}
		}

		if(rising)
		{
			if(transform.position.y == 5.0F)
			{
				rising = false;
			}
		}
	}

	public void RockPaperScissors()
	{
		playerBattling = true;
	}

	void CheckInput()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			counterAttack = 1;

			playerBattling = false;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			counterAttack = 2;

			playerBattling = false;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			counterAttack = 3;

			playerBattling = false;
		}
	}

	public void DamagePlayer()
	{
		Debug.Log("Damaged!");
		damaged = true;

		iTween.MoveTo(gameObject, new Vector3(transform.position.x, -15.0F, transform.position.z), 3.0F);

		bool above2 = GetComponent<scrPlayerMovement>().above;
		controller.Fall(above2);

		counterAttack = 0;
	}

	void Flash()
	{
		MeshRenderer mesh = GameObject.Find("FullHeroine").GetComponent<MeshRenderer>();
		mesh.enabled = !mesh.enabled;

		flashCounter++;

		if(flashCounter >= 10)
		{
			flashCounter = 0;
			CancelInvoke("Flash");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("Feather"))
		{
			feathers++;

			if(feathers >= 3)
			{
				rising = true;

				iTween.MoveTo(gameObject, new Vector3(transform.position.x, 5.0F, transform.position.z), 3.0F);

				controller.Rise();
				feathers = 0;
			}
		}

		if (other.tag.Equals("Obstacle")) 
		{
			Debug.Log("You hit an obstacle!");
			health--;

			InvokeRepeating("Flash", 0.0F, 0.2F);
		}
	}
}
