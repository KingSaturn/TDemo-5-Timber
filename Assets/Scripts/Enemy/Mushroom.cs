using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	//Mushroom Vairables
	//Prerequisites, the player must have a "Muhsroom" tag in unity.
	//The gameObject must have a character controller
	public static GameObject mushroom;
	private const string mushroom_tag = "Mushroom";
	private CharacterController Controller;
	private float speed = 50;
	private Vector3 velocity;


	private bool is_neutral;
	//Animation Block
	//The player must have an Animator component
	private Animator mushroom_animations;
	private int movement_animation_logger;    //Used for keeping track of if the mushroom is moving

	// Start is called before the first frame update
	void Start()
	{
		//PLayer    -Automatically aqquires objects no need to pass them in via public 
		mushroom = (GameObject.FindGameObjectsWithTag(mushroom_tag))[0];    //Going to need to be set differently when spawning in the mushroom objects.
		Controller = mushroom.GetComponent<CharacterController>();
		mushroom_animations = mushroom.GetComponent<Animator>();
		//Need to track and set the mushrooms state ie Aggressive or not
		//Animator value
		mushroom_animations.SetBool("Is_Neutral", true);
		is_neutral = true;

	}

	// Update is called once per frame
	void Update()
	{
//Triggers section, you will replace these if's with AI triggers corrosponding to the actions
	//Moving Foward
		if(Input.GetKeyDown(KeyCode.W ) )
		{
			Debug.Log("W");
			movement_animation_logger += 1;	//When pressed down, it registers the key to be used
		}
		
		else if(Input.GetKeyUp(KeyCode.W ) )
		{
			Debug.Log("No W");
			movement_animation_logger -= 1;	//Used to unregister the key, it will decrease the counter
		}
		
		else
		{
		}
	//Moving To The Left
		if(Input.GetKeyDown(KeyCode.A ) )
		{
			Debug.Log("No W");
			movement_animation_logger += 1;
		}
		
		else if(Input.GetKeyUp(KeyCode.A ) )
		{
			Debug.Log("No W");
			movement_animation_logger -= 1;
		}
		
		else
		{
		}
	//Moving To The Right
		if(Input.GetKeyDown(KeyCode.S ) )
		{
			Debug.Log("No W");
			movement_animation_logger += 1;
		}
		
		else if(Input.GetKeyUp(KeyCode.S ) )
		{
			Debug.Log("No W");
			movement_animation_logger -= 1;
		}
		
		else
		{
		}
	//Moving Backwards
		if(Input.GetKeyDown(KeyCode.D ) )
		{
			Debug.Log("No W");
			movement_animation_logger += 1;
		}
		
		else if(Input.GetKeyUp(KeyCode.D ) )
		{
			Debug.Log("No W");
			movement_animation_logger -= 1;
		}
		
		else
		{
		}
	//Attacking
		if(Input.GetKeyDown(KeyCode.E ) )
		{
			if(is_neutral== false )
			{
				Debug.Log("Attack" );
				mushroom_animations.SetTrigger("IsAttacking" );	//Pass in the parameter from the animator window it will trigger to start
			}
			
			else
			{
				Debug.Log("Cannot attack MUST TRANSFORM" );
			}
		}
		
		else
		{
		}
	//Talking
		if(Input.GetKeyDown(KeyCode.R )&& (movement_animation_logger== 0 ) )
		{
			switch(is_neutral )
			{
				case false:
					Debug.Log("Cannot talk" );
					break;
				
				default:
					Debug.Log("Talk" );
					mushroom_animations.SetTrigger("IsTalking" );
					break;
			}
		}
		
		else
		{
		}
	//Transforming 
		if(Input.GetKeyDown(KeyCode.T )&& (movement_animation_logger== 0 ) )
		{
			switch(is_neutral )
			{
				case false:
					Debug.Log("Cannot transform" );
					break;
				
				default:
					Debug.Log("Transform" );
					mushroom_animations.SetBool("Is_Neutral", false );
					is_neutral= false;
					
					mushroom_animations.SetTrigger("Transform" );
					break;
			}
			
		}
		
		else
		{
		}
	//Animation Playing block
		//Based on inputs to see if the animations can play or not 
		if(movement_animation_logger== 0 )	//When the counter is no longer 0 it is valid so walking stops
		{
			switch(is_neutral )
			{
				case false:
					mushroom_animations.SetBool("IsMoving" ,false );
					//Debug.Log("Idle but aggr" );
					break;
				
				default:
					mushroom_animations.SetBool("IsMoving" ,false );
					//Debug.Log("Plain Idle" );
					break;
			}
		}
		
		else
		{
			switch(is_neutral )
			{
				case false:
					mushroom_animations.SetBool("IsMoving" ,true );
					//Debug.Log("Aggr move" );
					break;
				
				default:
					mushroom_animations.SetBool("IsMoving" ,true );
					//Debug.Log("Plain move" );
					break;
			}
		}
		
	//Movement block
		float x= Input.GetAxis("Horizontal" );
		float z= Input.GetAxis("Vertical" );
		
		Vector3 move=transform.right* x+ transform.forward* z;

		Controller.Move(move* speed* Time.deltaTime );
		Controller.Move(velocity* Time.deltaTime );	
	}
}