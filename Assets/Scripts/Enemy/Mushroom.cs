using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
	//Mushroom Vairables
	//Prerequisites, the player must have a "Muhsroom" tag in unity.
	//The gameObject must have a character controller
	public static GameObject player;
	private CharacterInfo info;
	private const string mushroom_tag = "Mushroom";
	private CharacterController Controller;
	private Vector3 velocity;
	public float lookRadius;
	private SphereCollider rangeCollider;


	private bool is_neutral;
	private bool is_moving;
	//Animation Block
	//The player must have an Animator component
	private Animator mushroom_animations;
	private int movement_animation_logger;    //Used for keeping track of if the mushroom is moving

	// Start is called before the first frame update
	void Start()
	{
		//PLayer    -Automatically aqquires objects no need to pass them in via public 
		player = GameObject.FindGameObjectWithTag("Player");
		info = this.GetComponent<CharacterInfo>();
		Controller = this.GetComponent<CharacterController>();
		rangeCollider = this.GetComponent<SphereCollider>();
		mushroom_animations = this.GetComponent<Animator>();
		//Need to track and set the mushrooms state ie Aggressive or not
		//Animator value
		mushroom_animations.SetBool("Is_Neutral", true);
		is_neutral = true;
		is_moving = false;

	}

	// Update is called once per frame
	void Update()
	{
		//Triggers section, you will replace these if's with AI triggers corrosponding to the actions
		//Moving Foward
		if (is_moving)
		{
			movement_animation_logger = 1;
		}
		else
        {
			movement_animation_logger = 0;

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
		if(info.currentHp != info.maxHp.GetValue())
		{
			switch(is_neutral )
			{
				case false:
					break;
				
				default:
					mushroom_animations.SetBool("Is_Neutral", false );
					is_neutral= false;
					
					mushroom_animations.SetTrigger("Transform" );
					break;
			}
			
		}
		
		else
		{
		}

		//Movement block
		float distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance < lookRadius)
		{
			if (is_neutral)
			{
				FaceTarget();
			}
			if (!is_neutral)
			{
				is_moving = true;
				FaceTarget();
				Controller.Move(transform.forward * info.speed.GetValue() * Time.deltaTime);
			}
		}
		else
		{
			is_moving = false;
		}

		//Animation Playing block
		//Based on inputs to see if the animations can play or not 
		if (movement_animation_logger== 0 )	//When the counter is no longer 0 it is valid so walking stops
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
	}

    private void FaceTarget()
	{
		Vector3 direction = (player.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
	}

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}