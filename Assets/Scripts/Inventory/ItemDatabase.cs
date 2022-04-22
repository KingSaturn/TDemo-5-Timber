using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Timber.InventorySystem
{
    public static class ItemDatabase
    {
        private static List<ItemDatabaseSO> databases;
        public static ItemData GetItemData(int itemID)
        {
            if (databases == null)
            {
                LoadDatabases();
            }
            for (int i = 0; i < databases.Count; i++)
            {
                for (int j = 0; j < databases[i].items.Count; j++)
                {
                    if (databases[i].items != null)
                    {
                        if (databases[i].items[j].id == itemID)
                        {
                            return databases[i].items[j];
                        }
                    }
                }
            }
            return null;
        }
        private static void LoadDatabases()
        {
            databases = new List<ItemDatabaseSO>();
            ItemDatabaseSO[] loadedDatabases = Resources.LoadAll<ItemDatabaseSO>("Items");
            databases = loadedDatabases.ToList();
        }
    }
}