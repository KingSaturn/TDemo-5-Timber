using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player_scope
{
	public class Player : MonoBehaviour
	{
	//Player Vairables
		//Prerequisites, the player must have a "Player" tag in unity.
		//The gameObject must have a character controller
		public static GameObject player;
			private const string player_tag= "Player";
			private CharacterController Controller;
			//The stat class that will be used to determine things
			private PlayerInfo info;
			private Vector3 velocity;
			//Used to access the players hand for equipting the axe
			public static GameObject player_hand;
			public static GameObject inventory;
			public static Canvas inventory_canvas;
	//Animation Block
		//The player must have an Animator component
		private Animator human_animations;
			private int movement_animation_logger;	//Used to keep track if the player is moving or not
	//Camera
		//Must be in the game and tagged as the "MainCamera"
		private Camera mainCam;
			private const string mainCam_tag= "MainCamera";
	//Attack Components
		//Jan can explaim these
		public Transform enemyCheck;
		public LayerMask enemyMask;
	//Axe Vairables
		//There are two seperate gameobjects to account for their different properties, i.e The held one does not have a ridig body just a collider while thrown has both
		private GameObject axe;	//Used for tracking the current axe
			private const string axe_tag= "Axe";
		public GameObject held_axe_prefab;
		public GameObject thrown_axe_prefab;
			public static bool Has_axe; //Used to keep track of if the player has the axe in hand
			public static bool charging_axe;
		//Cooldown for picking up the axe so it doesn't insta-equip
		private float axe_pickup_timer;
		//Used to keep track of  how hard the axe will be thrown
		private float axe_throwing_power;
			private const float AXE_MIN_POWER= 2000.0f;	//Min value 
			private const float AXE_MAX_POWER= 10000.0f;	//Max value
			private const float AXE_INCRIMENT= 2000.0f;	//Stepping value
			//Timer vairables for the incrument in power, for as long as the button is held the timer will start counting down
			private float axe_incriment_timer;
				private const float HALF= 0.5f;
				private const float ZERO= 0.0f;
			
		//Variables for the axe.
		private Vector3 axe_position= new Vector3(-0.0465f, -0.021f, 0.0f );
		private Quaternion axe_rotation= new Quaternion(-0.194f, 2.155f, 275.215f, 0.0f );
				
	//Ground
		public LayerMask groundMask; //Make a ground parent object, put grond in there and assign the tag for raycasting.

		private ItemSpawner itemSpawner;
        // Start is called before the first frame update
        private void Awake()
        {
            //Player	-Automatically aqquires objects no need to pass them in via public 
			player=(GameObject.FindGameObjectsWithTag(player_tag) )[0];
				Controller=player.GetComponent<CharacterController>();
				human_animations=player.GetComponent<Animator>();
				info = player.GetComponent<PlayerInfo>();
				player_hand= GameObject.Find("Lumber_Jack/Armature/Hand_L/Hand_L_end");
				inventory = GameObject.Find("Lumber_Jack/Menus/Inventory");
			inventory_canvas = inventory.GetComponent<Canvas>();
		//Camera		
			mainCam=(GameObject.FindGameObjectsWithTag(mainCam_tag ) )[0 ].GetComponent<Camera>();
		//Axe	
			AxeParent.Parent_Axe(held_axe_prefab);
			axe =GameObject.FindGameObjectsWithTag("Axe")[0];
				Has_axe= true;
				charging_axe = false;
						
			axe_throwing_power= AXE_MIN_POWER;
				axe_incriment_timer= ZERO;
			transform.rotation = Quaternion.Euler(0, 0, 0);
				axe_pickup_timer = HALF;

			itemSpawner = FindObjectOfType<ItemSpawner>();
        }

        // Update is called once per frame
        void Update()
		{
			if (transform.position.y > 3.0f)
            {
				Controller.Move(new Vector3(0,-1,0));
            }
		//W Key
			if(Input.GetKeyDown(KeyCode.W ) )
			{
				movement_animation_logger+= 1;	//When pressed down, it registers the key to be used
			}
			
			else if(Input.GetKeyUp(KeyCode.W ) )
			{
				movement_animation_logger-= 1;	//Used to unregister the key, it will decrease the counter
			}
			
		//A Key
			if(Input.GetKeyDown(KeyCode.A ) )
			{
				movement_animation_logger+= 1;
			}
			
			else if(Input.GetKeyUp(KeyCode.A ) )
			{
				movement_animation_logger-= 1;
			}
			
		//S Key
			if(Input.GetKeyDown(KeyCode.S ) )
			{
				movement_animation_logger+= 1;
			}
			
			else if(Input.GetKeyUp(KeyCode.S ) )
			{
				movement_animation_logger-= 1;
			}

		//D Key
			if(Input.GetKeyDown(KeyCode.D ) )
			{
				movement_animation_logger+= 1;
			}
			
			else if(Input.GetKeyUp(KeyCode.D ) )
			{
				movement_animation_logger-= 1;
			}
			
		//E Key ATTACK BUTTON
			if(Input.GetKeyDown(KeyCode.E ) )
			{
				if(Has_axe== true )
				{
					human_animations.SetTrigger("isAttacking" );	//Pass in the parameter from the animator window it will trigger to start
				}
				
				else
				{
				}
			}
			
		//R Key TALKING BUTTON
			if(Input.GetKeyDown(KeyCode.R )&& (movement_animation_logger== 0 ) )
			{
				human_animations.SetTrigger("isTalking" );
			}
			
		//T Key IDLE TRIGGER, please set to something else when in game.
			//Meant to be used if the player stops movement or something
			if(Input.GetKeyDown(KeyCode.T )&& (movement_animation_logger== 0 ) )
			{
				human_animations.SetTrigger("isIdle" );
				itemSpawner.SpawnItem(new Vector3(-21, 45, -93), 1, 1);
			}
			//Animation Playing block
			//Based on inputs to see if the animations can play or not 
			if (movement_animation_logger > 0)  //When the counter is no longer 0 it is valid so walking stops
			{
				human_animations.SetBool("isMoving", true);
			}

			else
			{
				human_animations.SetBool("isMoving", false);
			}
				
			if (PauseMenu.isPaused)
			{
				return;
			}

			//Mouse One THROW CHARGE UP CODE
			if (Input.GetMouseButtonDown(1))
			{
				//Charging Up Throw
				if (Has_axe == true)
				{
					charging_axe = true;
				}
			}
			if (charging_axe)
            {
				axe_incriment_timer += Time.deltaTime;


				// Jan's code, if you wanna try and fix it somehow, go ahead
				//if (axe_incriment_timer < ZERO)
				//{
				//	switch (axe_throwing_power)
				//	{
				//		case AXE_MAX_POWER:
				//			break;

				//		default:
				//			axe_throwing_power += AXE_INCRIMENT;
				//			break;
				//	}
				//}
			}
		
			// Makes the cooldown timer for picking back up the axe function.
			if(Has_axe== false)
            {
				axe_pickup_timer -= Time.deltaTime;
			}
			// Gives the man the axe if there is no axe in the world
			if ((GameObject.FindGameObjectsWithTag(axe_tag)[0]) == null && Has_axe == true)
            {
				AxeParent.Parent_Axe(held_axe_prefab);
				axe = (GameObject.FindGameObjectsWithTag(axe_tag)[0]);

			}
		//For releasing the axe	ACTUAL THROWING CODE
			if (Input.GetMouseButtonUp(1 ) )
			{
				if(Has_axe== true )
				{
					human_animations.SetTrigger("isAttacking" );
				//Cleanup
					charging_axe = false;
					Has_axe = false;
					Destroy(GameObject.Find("held_axe" ) );
					inventory_canvas.enabled = false;

					axe_throwing_power = (axe_incriment_timer * AXE_MAX_POWER + AXE_MIN_POWER);
					if (axe_throwing_power > AXE_MAX_POWER)
                    {
						axe_throwing_power = AXE_MAX_POWER;
					}
					
					Axe_Thrower(player, thrown_axe_prefab, axe_throwing_power );
					axe=GameObject.FindGameObjectsWithTag(axe_tag )[0 ];	//Updates the axe value
										
					axe_throwing_power= AXE_MIN_POWER;  //Reset for the next one
					axe_incriment_timer = ZERO;
				}
				
				else
				{
					axe_throwing_power= AXE_MIN_POWER;
					axe_incriment_timer = ZERO;
				}
			}
		//Movement block
			float x= Input.GetAxis("Horizontal" );
			float z= Input.GetAxis("Vertical" );
			
			Vector3 move= transform.right* x+ transform.forward* z;

			// Account for diagonal movement
			if (x != 0 && z != 0)
            {
				move = new Vector3(move.x * 0.5f, move.y, move.z * 0.5f);
            }

			Controller.Move(move* info.speed.GetValue()* Time.deltaTime );
			Controller.Move(velocity* Time.deltaTime );
		//Aiming block used for updating the rotation value for the player
			Aim();	
		}
		
	//Called when a player collides with something, the collision subject is the thing that hits the player
		private void OnCollisionEnter(Collision collision_subject )
		{
			switch(collision_subject.gameObject.tag )
			{
				case "Axe":
					if (axe_pickup_timer < ZERO)
                    {
						if (Has_axe == true)
						{
							Destroy(collision_subject.gameObject);  //Just in case there is already an axe in hand it will kill any axe not meant to exist
							axe_pickup_timer = HALF;
							break;
						}

						else
						{
							//Delete Axe in world then begin to parent it to the player
							Destroy(collision_subject.gameObject);

							//Reparent the axe to the hand again.
							AxeParent.Parent_Axe(held_axe_prefab);
							inventory_canvas.enabled = true;
							axe = GameObject.FindGameObjectsWithTag(axe_tag)[0];
							Has_axe = true;
							axe_pickup_timer = HALF;
							break;
						}

					}
					else
                    {
						break;
                    }
					
				case "Ground":
					break;
				
				default:
					break;
			}
		}
		
	//Used for spawing in Axes that are thrown by the player
		public void Axe_Thrower(GameObject player, GameObject axe_prefab, float launch_velocity )
		{				
			//Gets the players current position
			Vector3 current_location= player.transform.position;
			//Apply offset to axe throwing
			current_location= (new Vector3(current_location.x , current_location.y+15f, current_location.z ) )+ transform.forward*10;
			//Gets the current mouse position in the world space, then the players and normalises it to get a rotation value for which way the axe should be thrown
			Vector3 mouse_position = GetMouse();
			Vector3 relative_position= mouse_position- player.transform.position;
			Quaternion current_rotation= Quaternion.LookRotation(relative_position );

			//Actual throwing of the axe, DO NOT CHANGE THE AXE NAMES ELSE SOME CODE WILL BREAK FOR CLEAN UP OF THE AXES
			GameObject thrown_axe= Instantiate(thrown_axe_prefab, current_location, current_rotation ) as GameObject;
			ThrownAxe axeScript = thrown_axe.GetComponent<ThrownAxe>();
			thrown_axe.name= "thrown_axe";
			//Adds speed to the axe so it can be launched
			if (axeScript.inWall == false)
            {
				thrown_axe.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 1000, launch_velocity));
			}
			if (axeScript.inWall)
            {
				thrown_axe.transform.position = transform.position - transform.forward *25 + transform.up*20;
			}				
			axe = thrown_axe;
		}
		
	// Check weapon range
		private void OnDrawGizmosSelected()
		{
			if(enemyCheck == null)
				return;

			Gizmos.DrawSphere(enemyCheck.position, info.attackRange.GetValue());   
		}
		
	//Used to get the right rotation for the axe throwing
		public Vector3 GetMouse()
		{
			var ray = mainCam.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
			{
				return (hitInfo.point );
			}
			
			return (Vector3.zero );
		}
		
	// Rotation functions
		private (bool success, Vector3 position) GetMousePosition()
		{
		// Creates a ray variable to store the info from the ray, it defines the parameters to 'shoot', being from the camara to, in this case, the mouse position.
			var ray = mainCam.ScreenPointToRay(Input.mousePosition);

		// With Physics the ray gets actually casts so that information can be obtained from it, it takes the parameters of the ray, being the camara to the mouse. Then
		// it creates a variable to store the Vector3 that results from the colision of the ray. I am guessing mathf infinity is to set the limit distance of the ray, in this case
		// infinite. Finally groundMask is to set the objects the ray can collide with. In our game being the ground layer.
			if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
			{
			// The Raycast hit something, return with the position
				return (success: true, position: hitInfo.point);
			}
			else
			{
			// The Raycast did not hit anything, return Vector3(0, 0, 0) and false so that Aim() doesn't happen. 
				return (success: false, position: Vector3.zero);
			}
		}
		
	//Aiming fucntion 
		private void Aim()
		{
			var (success, position) = GetMousePosition();
			if (success)
			{
			// In the case it gets a success, then store the position of the mouse - the player current position. This gives the direction to look at... for some reason.e
				var direction = position - player.transform.position;

			// Ignore the height difference
				direction.y = 0;

			// Make the player look at the direction.
				player.transform.forward = direction;
			}
		}

        private void OnLevelWasLoaded(int level)
        {
			if (axe != null)
            {
				AxeParent.Parent_Axe(held_axe_prefab);
            }
		}

    }
	
}
