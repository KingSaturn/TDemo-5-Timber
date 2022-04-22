using System.Collections.Generic;
using Timber.InventorySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Database")]
public class ItemDatabaseSO : ScriptableObject
{

    public List<ItemData> items;

}
