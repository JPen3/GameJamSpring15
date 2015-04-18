using UnityEngine;
using System.Collections;

public class scrWizard : MonoBehaviour 
{
	private float battleTimer = 0.0F; //Battle timer (counts up)
	private float battleTime = 0.0F; //Time the battle will happen next

	public bool battling = false; //Battle phase

	public int attack = 0;

	//Use this for initialization
	void Start () 
	{
		battleTime = Random.Range(10, 20);
	}
	
	//Update is called once per frame
	void Update () 
	{
		if(!battling)
		{
			BattleTimer();
		}
	}

	void BattleTimer()
	{
		//decrease the blink timer
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

		//Pick a number between 0 and 2
		attack = Random.Range(0,3);
		Debug.Log(attack);

		switch(attack)
		{
			case 0:
				//Attack 1
				StartCoroutine(AttackOne(2.0F));
				break;
			case 1:
				//Attack 2
				StartCoroutine(AttackTwo(2.0F));
				break;
			case 2:
				//Attack 3
				StartCoroutine(AttackThree(2.0F));
				break;
		}
	}

	IEnumerator AttackOne(float activateTime)
	{
		yield return new WaitForSeconds(activateTime);

		Debug.Log("AttackOneOver");
		battling = false;
	}

	IEnumerator AttackTwo(float activateTime)
	{
		yield return new WaitForSeconds(activateTime);

		Debug.Log("AttackTwoOver");
		battling = false;
	}

	IEnumerator AttackThree(float activateTime)
	{
		yield return new WaitForSeconds(activateTime);

		Debug.Log("AttackThreeOver");
		battling = false;
	}
}
