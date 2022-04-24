using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private CharacterInfo info;
    private Collider hitbox;
    private bool inRange = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        info = player.GetComponent<CharacterInfo>();
        hitbox = this.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player.name)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == player.name)
        {
            inRange = false;
        }
    }

    public void DealDamage(int damage)
    {
        if (inRange)
        {
            info.TakeDamage(damage);
        }

    }
}
