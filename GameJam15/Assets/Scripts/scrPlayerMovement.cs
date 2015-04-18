using UnityEngine;
using System.Collections;

public class scrPlayerMovement : MonoBehaviour 
{
	public scrWizard wizardScript;
	public scrPlayer playerScript;

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
		above = false;
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
				Idle(above);
				break;
			case PlayerState.FLY:
				//Check game layer
				Fly(above);

				//Apply translation/movement
				float transVertical = Input.GetAxis("Vertical") * flySpeed;
				float transHorizontal = Input.GetAxis("Horizontal") * flySpeed;
				transVertical *= Time.deltaTime;
				transHorizontal *= Time.deltaTime;
				transform.Translate(transHorizontal, transVertical, 0);

				//Clamp position
				if(above)
				{
					transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.0F, 10.0F), Mathf.Clamp(transform.position.y, 0.0F, 10.0F), 0);
				}
				else
				{
					transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.0F, 10.0F), Mathf.Clamp(transform.position.y, -20.0F, -10.0F), 0);
				}
	
				break;
			case PlayerState.FALL:
				Fall(above);
				break;
			case PlayerState.RISE:
				Rise();
				break;
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
		return playerScript.damaged;
	}

	//Is the player rising?
	bool Rising()
	{
		//Collect enough feathers?
		return playerScript.rising;
	}

	//Is the player battling?
	bool Battling()
	{
		//Rock Paper Scissor Initiated
		return wizardScript.battling;
	}

	//Idle state above clouds
	void Idle(bool above)
	{
		if(!idling) 
		{
			SetFlySpeed(0.0F); //Set player speed to 0
		}

		if(above)
		{
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
		else
		{
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
	}

	void Fly(bool above)
	{
		if(!flying) 
		{
			SetFlySpeed(10.0F); //Set player speed to 6
		}

		if(above)
		{
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
		else
		{
			if(Rising())
			{
				state = PlayerState.RISE;
			}
			else if(!Flying())
			{
				state = PlayerState.IDLE;
			}
		}
	}

	//Sets player speed once
	void SetFlySpeed(float speed) 
	{
		flySpeed = speed;
		
		idling = state == PlayerState.IDLE;
		flying = state == PlayerState.FLY;
		battling = state == PlayerState.BATTLE;
		falling = state == PlayerState.FALL;
		rising = state == PlayerState.RISE;
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

	void Fall(bool above)
	{
		if(!falling)
		{
			SetFlySpeed(0.0F);
		}

		if(above)
		{
			if(!Falling())
			{
				this.above = false;
				state = PlayerState.IDLE;
			}
		}
		else
		{
			Debug.Log("YOU LOSE!");
		}
	}
	
	void Rise()
	{
		if(!rising)
		{
			SetFlySpeed(0.0F);
		}

		if(!Rising())
		{
			Debug.Log("Done Rising");
			above = true;
			state = PlayerState.IDLE;
		}
	}
}

public enum PlayerState {IDLE, FLY, RISE, FALL, BATTLE};