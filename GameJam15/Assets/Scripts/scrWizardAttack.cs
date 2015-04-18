using UnityEngine;
using System.Collections;

public class scrWizardAttack : MonoBehaviour 
{
	public int attackType;

	// Use this for initialization
	void Start () 
	{
		if(attackType == 0)
		{
			StartCoroutine(RockAttack(3.2F));
		}
		else if(attackType == 1)
		{
			StartCoroutine(PaperAttack(3.2F));
		}
		else if(attackType == 2)
		{
			StartCoroutine(ScissorsAttack(3.2F));
		}
	}

	IEnumerator RockAttack(float attackTime)
	{
		yield return new WaitForSeconds(attackTime);

		Destroy(gameObject);
	}

	IEnumerator PaperAttack(float attackTime)
	{
		yield return new WaitForSeconds(attackTime);

		Destroy(gameObject);
	}

	IEnumerator ScissorsAttack(float attackTime)
	{
		yield return new WaitForSeconds(attackTime);

		Destroy(gameObject);
	}
}
