using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player_scope
{
	public class AxeParent : MonoBehaviour
	{
	//Attatched to the thrown axe, used to reparent to the hand of the lumberjack
		public static void Parent_Axe(GameObject axe_prefab )
		{
			
			if(Player.Has_axe== false || (Player.Has_axe == true && GameObject.Find("Lumber_Jack/Armature/Hand_L/held_axe") == null))
			{	
				Vector3 axe_position= new Vector3(0.0f, 0.0f, 0.0f );
				Quaternion axe_rotation= Quaternion.Euler(-0.194f, 2.155f, 275.215f );
				
				GameObject axe= Instantiate(axe_prefab, Player.player_hand.transform.parent, false) as GameObject;
				axe.name= "held_axe";
				axe.transform.localScale= new Vector3(0.007564979f, 0.00756498f, 0.007564981f );	//Scales object
			}

			else
			{
				
			}
		}

        private void Start()
        {
			transform.Rotate(90.0f, 0.0f, 0.0f);
        }
    }
	
}
