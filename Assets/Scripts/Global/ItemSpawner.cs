using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static void SpawnItem(Vector3 location, int id, int amount)
    {
        GameObject item = Resources.Load("Prefabs/Item") as GameObject;
        GameObject spawnedItem = Instantiate(item, location, Quaternion.Euler(0, 0, 0));
        WorldItem itemData = spawnedItem.GetComponent<WorldItem>();
        itemData.SetWorldItem(id, amount);
    }
}
