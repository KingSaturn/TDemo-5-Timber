using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player_scope;

public class PlayerAttack : MonoBehaviour
{
    private Player playerScript;
    private PlayerInfo attackInfo;
    public List<GameObject> attackers;

    private void Awake()
    {
        playerScript = this.GetComponentInParent<Player>();
        attackInfo = this.GetComponentInParent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterInfo>() != null)
        {
            GameObject x = other.gameObject as GameObject;
            attackers.Add(x);
            Debug.Log("Yeah");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attackers.Contains(other.gameObject))
        {
            attackers.Remove(other.gameObject);
        }
    }

    public void DamageEnemy(int damage)
    {
        List<GameObject> attacked = new List<GameObject>();
        List<GameObject> killed = new List<GameObject>();
        foreach(GameObject enemy in attackers)
        {
            if (enemy == null)
            {
                killed.Add(enemy);
                continue;
            }    
            if(enemy.GetComponent<CharacterInfo>() != null && !attacked.Contains(enemy))
            {
                attacked.Add(enemy);
                CharacterInfo info = enemy.GetComponent<CharacterInfo>();
                info.TakeDamage(attackInfo.attack.GetValue());
            }
        }
        foreach(GameObject enemy in killed)
        {
            attackers.Remove(enemy);
        }
    }
}
