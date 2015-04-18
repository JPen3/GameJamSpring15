using UnityEngine;
using System.Collections;

public class scrWizard : MonoBehaviour 
{
	public GameObject rockAttackObj; //Reference to rock attack
	public GameObject paperAttackObj; //Reference to paper attack
	public GameObject scissorsAttackObj; //Reference to scissors attack

	public GameObject playerObj; //Reference to player object
	public scrCamera cameraScript; //Reference to the camera script

	private Vector3 spawnPos = new Vector3 (0, 5.0F, 25.0F); //Origin location
	private Vector3 offset = new Vector3(0, 0, 6.0F); //Move right in front of player
	private Vector3 targetPosition;

	private float battleTimer = 0.0F; //Battle timer (counts up)
	private float battleTime = 0.0F; //Time the battle will happen next

	public bool battling = false; //Battle phase
	public bool readyToAttack = false;
	public bool battleWon = false;

	public int attack = 0;

	//FOR iTWEEN MOVEMENT
	private int pointCount = 2;
	private float pathLength = 10;
	private float pointDeviation = 3.0F;
	private Vector3[] path = null;
	private Vector3 rootPosition;

	public int health = 5;

	//Use this for initialization
	void Start () 
	{
		battleTime = Random.Range(5, 10);
	}

	//Reset wizard properties
	void OnEnable()
	{
		Debug.Log("Reset");
		battling = false;
		battleTimer = 0.0F;
		attack = 0;
		readyToAttack = false;
		path = null;
		battleTime = Random.Range(5, 10);
	}
	
	//Update is called once per frame
	void Update () 
	{
		if(!battling)
		{
			BattleTimer();
		}
		else
		{
			if(!readyToAttack)
			{
				if(transform.position == targetPosition)
				{
					readyToAttack = true;
					Attack();
				}
			}
		}
	}

	void BattleTimer()
	{
		//Increase battle timer
		battleTimer += Time.deltaTime;

		if (battleTimer >= battleTime)
		{
			battleTimer = 0.0F;
			Battle();
		}
	}

	void Battle()
	{
		battling = true;

		//Find players position
		targetPosition = playerObj.transform.position + offset;

		//cameraScript.StartBattleCamera();

		//Move wizard to player
		GenerateRandomPath(0, targetPosition);
		iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 3, "easetype", iTween.EaseType.easeInOutCubic));
	}

	/*
	void OnDrawGizmos()
	{
		if (path != null) 
		{
			if(path.Length > 0)
			{
				iTween.DrawPath(path);	
			}	
		}	
	}
	*/

	void GenerateRandomPath(int zDirection, Vector3 endPoint)
	{
		rootPosition = transform.position;
		path = new Vector3[pointCount+2];
		float pointGap = pathLength/pointCount;
		path[0] = rootPosition;
		path[pointCount+1] = endPoint;

		for (int i = 1; i < pointCount+1; i++) 
		{
			float randomX = rootPosition.x + Random.Range(-pointDeviation,pointDeviation);
			float randomY = rootPosition.y + Random.Range(-pointDeviation,pointDeviation);
			float newZ;
			if(zDirection == 0)
			{
				newZ = rootPosition.z - (pointGap*i);
			}
			else
			{
				newZ = rootPosition.z + (pointGap*i);
			}
			path[i] = new Vector3(randomX, randomY, newZ);
		}
	}	

	void Attack()
	{
		//Pick a number between 1 and 3
		attack = Random.Range(1,3);
		Debug.Log(attack);
		
		switch(attack)
		{
			case 1:
				//Attack 1
				StartCoroutine(RockAttack(2.0F));
				break;
			case 2:
				//Attack 2
				StartCoroutine(PaperAttack(2.0F));
				break;
			case 3:
				//Attack 3
				StartCoroutine(ScissorsAttack(2.0F));
				break;
		}

		playerObj.GetComponent<scrPlayer>().RockPaperScissors();
	}

	void DamageWizard()
	{
		health--;
		Debug.Log("Wizard Health: " + health);

		//Reset player counterAttack
		playerObj.GetComponent<scrPlayer>().counterAttack = 0;

		if(health == 0)
		{
			//Player wins!
			Debug.Log("YOU WIN!");
		}
	}

	void CheckResults()
	{
		Debug.Log("Counter: " + playerObj.GetComponent<scrPlayer> ().counterAttack);
		if(playerObj.GetComponent<scrPlayer>().counterAttack == attack)
		{
			battleWon = false;

			//Damage Wizard
			DamageWizard();
		}
		else
		{
			battleWon = true;

			//Damage player
			playerObj.GetComponent<scrPlayer>().DamagePlayer();
		}

		battling = false;
		readyToAttack = false;
		
		if(!battleWon)
		{
			GenerateRandomPath(1, spawnPos);
			iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 3, "easetype", iTween.EaseType.easeInOutCubic));
		}

		//cameraScript.EndBattleCamera();
	}
	
	IEnumerator RockAttack(float activateTime)
	{
		//Spawn rock attack
		GameObject rockAttack = Instantiate(rockAttackObj, new Vector3(transform.position.x, transform.position.y + 2.0F, transform.position.z), transform.rotation) as GameObject;
		rockAttack.name = "RockAttack";

		yield return new WaitForSeconds(activateTime);

		CheckResults();
		Debug.Log ("RockAttackOver");
	}

	IEnumerator PaperAttack(float activateTime)
	{
		//Spawn paper attack
		GameObject paperAttack = Instantiate(paperAttackObj, new Vector3(transform.position.x, transform.position.y + 2.0F, transform.position.z), transform.rotation) as GameObject;
		paperAttack.name = "PaperAttack";

		yield return new WaitForSeconds(activateTime);

		CheckResults();
		Debug.Log("PaperAttackOver");


	}

	IEnumerator ScissorsAttack(float activateTime)
	{
		//Spawn scissors attack
		GameObject scissorsAttack = Instantiate(scissorsAttackObj, new Vector3(transform.position.x, transform.position.y + 2.0F, transform.position.z), transform.rotation) as GameObject;
		scissorsAttack.name = "PaperAttack";

		yield return new WaitForSeconds(activateTime);

		CheckResults();
		Debug.Log("ScissorAttackOver");
	}
}
