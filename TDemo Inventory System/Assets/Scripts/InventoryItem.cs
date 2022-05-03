namespace Timber.InventorySystem
{
    public class InventoryItem : ItemData
    {
        public int currentStack;
        public InventoryItem(ItemData data)
        {
            if(data == null)
            {
                return;
            }
            id = data.id;
            icon = data.icon;
            maxStack = data.maxStack;
            name = data.name;
            itemValue = data.itemValue;
        }
        public InventoryItem(InventoryItem data)
        {
            if (data == null)
            {
                return;
            }
            id = data.id;
            icon = data.icon;
            maxStack = data.maxStack;
            name = data.name;
            currentStack = data.currentStack;
            itemValue = data.itemValue;
        }
    }
}