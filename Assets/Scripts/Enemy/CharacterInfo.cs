using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [Tooltip("Realtime variable for hp. Do not touch.")]
    public int currentHp;

    [Tooltip("Determines how fast a character will fall. Base: -9.81")]
    public float gravity;

    [Tooltip("Determines the starting and max health of a character.")]
    public Stat maxHp;

    [Tooltip("Determines the damage a character will deal.")]
    public Stat attack;

    [Tooltip("Determines how fast a character is.")]
    public Stat speed;

    private void Awake()
    {
        currentHp = maxHp.GetValue();
    }

    public void TakeDamage(int damage)
    {
        currentHp -= Mathf.Clamp(damage, 0, int.MaxValue);
        Debug.Log(currentHp);
    }

    public void DeathState()
    {
        return;
    }
}
