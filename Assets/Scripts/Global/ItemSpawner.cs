using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public void SpawnItem(Vector3 location, int id, int amount)
    {
        GameObject item = Resources.Load("Prefabs/Item") as GameObject;
        WorldItem itemData = item.GetComponent<WorldItem>();
        itemData = new WorldItem(id, amount);
        Instantiate(item, location, Quaternion.Euler(0, 0, 0));
    }
}
