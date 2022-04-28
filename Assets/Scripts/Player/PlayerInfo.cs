using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    public HealthBar healthBar;

    [Tooltip("Determines how fast the player can attack. Specific to 'PlayerInfo' script.")]
    public Stat attackSpeed;

    [Tooltip("Determines how far the player can attack from. Specific to 'PlayerInfo' script.")]
    public Stat attackRange;

    [Tooltip("Determines how many spaces are available in the player's inventory. Specific to 'PlayerInfo' script.")]
    public Stat inventorySize;

    [Tooltip("Determines how ethical the player is towards the forest. Specific to 'PlayerInfo' script.")]
    public Stat ethics;

    private void Awake()
    {
        healthBar = this.GetComponentInChildren<HealthBar>();
        currentHp = maxHp.GetValue();
    }
  
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= Mathf.Clamp(damage, 0, int.MaxValue);
        healthBar.UpdateHealth();
        Debug.Log(currentHp);
    }

}
