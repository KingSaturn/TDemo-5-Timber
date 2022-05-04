using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : CharacterInfo
{
    public int itemSpawnID;
    private Ethics ethics;

    private void Start()
    {
        ethics = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Ethics>();
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= Mathf.Clamp(damage, 0, int.MaxValue);
        if (currentHp <= 0)
        {
            ethics.ethicsDown(2.0f);
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
            ItemSpawner.SpawnItem(newPosition, itemSpawnID, UnityEngine.Random.Range(1, 3));
            Destroy(this.gameObject);
        }
    }
}
