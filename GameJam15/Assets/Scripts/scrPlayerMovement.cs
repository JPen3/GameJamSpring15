using UnityEngine;
using System.Collections;

public class scrPlayerMovement : MonoBehaviour 
{
	public scrWizard wizardScript;

	//current state of player
	public PlayerState state = PlayerState.IDLE;

	//Flying
	public bool idling;
	public bool flying;
	public bool rising;
	public bool falling;
	public bool battling;
	public bool above;

	private float flySpeed = 0.0F;

	// Use this for initialization
	void Start () 
	{
		above = true;
		idling = true;
	}
	
	//Update is called once per frame
	void Update () 
	{
		//Player movement state system
		switch (state) 
		{
			case PlayerState.IDLE:
				//Check game layer
				if(above)
				{
					IdleAbove();
				}
				else
				{
					IdleBelow();
				}
				break;
			case PlayerState.FLY:
				//Check game layer
				if(above)
				{
					FlyAbove();
				}
				else
				{
					FlyBelow();
				}

				//Apply translation/movement
				float transVertical = Input.GetAxis("Vertical") * flySpeed;
				float transHorizontal = Input.GetAxis("Horizontal") * flySpeed;
				transVertical *= Time.deltaTime;
				transHorizontal *= Time.deltaTime;
				transform.Translate(transHorizontal, transVertical, 0);

				//Clamp position
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.0F, 10.0F), Mathf.Clamp(transform.position.y, 0.0F, 10.0F), 0);
	
				break;
			/*
			case PlayerState.FALL;
				Fall();
				break;
			case PlayerState.RISE;
				Rise();
				break;
			*/
			case PlayerState.BATTLE:
				Battle();
				break;
		}
	}

	//Is the player flying?
	bool Flying() 
	{
		return(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
	}

	//Is the player falling?
	bool Falling()
	{
		//Lose to battle?
		return false;
	}

	//Is the player rising?
	bool Rising()
	{
		//Collect enough feathers?
		return false;
	}

	//Is the player battling?
	bool Battling()
	{
		//Rock Paper Scissor Initiated
		return wizardScript.battling;
	}

	//Idle state above clouds
	void IdleBelow()
	{
		if(!idling) 
		{
			SetFlySpeed(0.0F); //Set player speed to 0
		}

		//Do nothing unless state changes
		if(Rising())
		{
			state = PlayerState.RISE;
		}
		else if(Falling())
		{
			state = PlayerState.FALL;
		}
		else if(Flying()) 
		{
			state = PlayerState.FLY;
		}
	}

	//Idle state below clouds
	void IdleAbove()
	{
		if(!idling) 
		{
			SetFlySpeed(0.0F); //Set player speed to 0
		}

		//Do nothing unless state changes
		if(Falling())
		{
			state = PlayerState.FALL;
		}
		else if(Battling()) 
		{
			state = PlayerState.BATTLE;
		}
		else if(Flying())
		{
			state = PlayerState.FLY;
		}
	}

	void FlyBelow()
	{
		if(!flying) 
		{
			SetFlySpeed(10.0F); //Set player speed to 6
		}

		if(Rising())
		{
			state = PlayerState.RISE;
		}
		else if(!Flying())
		{
			state = PlayerState.IDLE;
		}
	}

	void FlyAbove()
	{
		if(!flying) 
		{
			SetFlySpeed(10.0F); //Set player speed to 6
		}
		
		if(Falling())
		{
			state = PlayerState.FALL;
		}
		else if(Battling())
		{
			state = PlayerState.BATTLE;
		}
		else if(!Flying())
		{
			state = PlayerState.IDLE;
		}
	}

	//Sets player speed once
	void SetFlySpeed(float speed) 
	{
		flySpeed = speed;
		
		idling = state == PlayerState.IDLE;
		flying = state == PlayerState.FLY;
		battling = state == PlayerState.BATTLE;
	}

	void Battle()
	{
		if(!battling)
		{
			SetFlySpeed(0.0F);
		}

		if(Falling())
		{
			state = PlayerState.FALL;
		}
		else if(!Battling())
		{
			state = PlayerState.IDLE;
		}
	}
}

public enum PlayerState {IDLE, FLY, RISE, FALL, BATTLE};