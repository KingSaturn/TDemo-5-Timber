using UnityEngine;
namespace Timber.InventorySystem
{
    [System.Serializable()]
    public class ItemData
    {
        public int id;
        public string name;
        public Sprite icon;
        public int maxStack;
        public float itemValue;

        public ItemData()
        {
            id = -1;
            name = "n/a";
            icon = null;
            maxStack = 0;
            itemValue = -1;
        }
    }
}