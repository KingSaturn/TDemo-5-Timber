using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    [Tooltip("Determines how fast the player can attack. Specific to 'PlayerInfo' script.")]
    public Stat attackSpeed;

    [Tooltip("Determines how far the player can attack from. Specific to 'PlayerInfo' script.")]
    public Stat attackRange;

    [Tooltip("Determines how many spaces are available in the player's inventory. Specific to 'PlayerInfo' script.")]
    public Stat inventorySize;

    [Tooltip("Determines how ethical the player is towards the forest. Specific to 'PlayerInfo' script.")]
    public Stat ethics;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
