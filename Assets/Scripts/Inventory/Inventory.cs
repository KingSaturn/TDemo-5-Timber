using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

namespace Timber.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public event Action OnIvnChange;
        public List<InventoryItem> items;
        //subject to change
        public int maxInventorySize = 20;
        private void Awake()
        {
            if (File.Exists(Path.Combine(Application.dataPath + "/SaveData.txt")))
            {
                Debug.Log("Loaded Items");
                string input = File.ReadAllText(Path.Combine(Application.dataPath + "/SaveData.txt"));
                SaveData data = JsonUtility.FromJson<SaveData>(input);
                items = data.items;
                for (int x = 0; x < items.Count; x += 1)
                {
                    items[x].icon = ItemDatabase.GetItemData(items[x].id).icon;
                    items[x].material = ItemDatabase.GetItemData(items[x].id).material;
                    items[x].model = ItemDatabase.GetItemData(items[x].id).model;
                }
                OnIvnChange?.Invoke();
            }
            else
            {
                Debug.Log("No Save");
                items = new List<InventoryItem>();
            }
        }

        private void Start()
        {
            OnIvnChange?.Invoke();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (File.Exists(Path.Combine(Application.dataPath + "/SaveData.txt")))
                {
                    Debug.Log("Save Deleted");
                    File.Delete(Path.Combine(Application.dataPath + "/SaveData.txt"));
                }
            }
        }

        public void SwapItems(int firstItemIndex, int secondItemIndex)
        {
            InventoryItem index1Copy = new InventoryItem(items[firstItemIndex]);
            items[firstItemIndex] = new InventoryItem(items[secondItemIndex]);
            items[secondItemIndex] = new InventoryItem(index1Copy);
            OnIvnChange?.Invoke();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>stacks to add if not enough space</returns>
        public void AddItem(int itemID, int itemAmount)
        {
            for (int i = 0; i < itemAmount; i++)
            {
                AddOneItem(itemID);
            }
        }
        /// <returns> True if item is added to new stack</returns>
        private bool AddOneItem(int itemID)
        {
            //check if there's space in inventory
            if (CheckSpace(itemID, 1) == false)
            {
                return false;
            }
            if (items.Count == 0)
            {
                AddItemToNewSlot(itemID);
                return true;
            }
            for (int i = 0; i < maxInventorySize; i++)
            {
                if(i >= items.Count)
                {
                    AddItemToNewSlot(itemID);
                    return true;
                }
                //add item to existing stack if there's space
                if (items[i].id == itemID)
                {
                    if (items[i].currentStack < items[i].maxStack)
                    {
                        items[i].currentStack++;
                        OnIvnChange?.Invoke();
                        return true;
                    }
                }
                //add item to empty slot
                else if (i == maxInventorySize)
                {
                    AddItemToNewSlot(itemID);
                    return true;
                }
            }
            return false;
        }
        private bool CheckSpace(int itemID, int itemCount)
        {
            if (items.Count + itemCount < maxInventorySize)
            {
                return true;
            }
            int stacks = itemCount;
            for (int i = 0; i < items.Count; i++)
            {
                if (stacks <= 0)
                {
                    return true;
                }
                if (items[i].id == itemID)
                {
                    int max = items[i].maxStack;
                    int current = items[i].currentStack;
                    int result = (max - current) - stacks;
                    stacks -= result;
                }
                else if(IsEmptySlot(i))
                {
                    return true;
                }
                else if(items[i].id != itemID)
                {
                    continue;
                }
            }
            return stacks <= 0;
        }
        public int GetItemCount(int itemID)
        {
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (itemID == items[i].id)
                {
                    itemCount = +items[i].currentStack;
                }
            }
            return itemCount;
        }
        private bool IsEmptySlot(int index)
        {
            return items[index].id == -1;
        }
        public void RemoveItemAt(int index, int amount)
        {
            if(ValidateIndex(index) == false)
            {
                return;
            }
            for (int i = 0; i < amount; i++)
            {
                RemoveSingleItem(0, index);
            }
        }
        public bool ValidateIndex(int index)
        {
            return index >= 0 && index < items.Count;
        }
        /// <summary>
        ///  for loop to remove item
        /// </summary>
        public bool RemoveItem(int itemID, int itemAmount)
        {
            for (int i = 0; i < itemAmount; i++)
            {
                if (RemoveSingleItem(itemID) == false)
                {
                    return false;
                }
            }
            return true;
        }
        private bool RemoveSingleItem(int itemID, int index)
        {
            if (index < 0)
            {
                index = GetFirstItemIndex(itemID);
            }
            if (index == -1)
            {
                return true;
            }
            else if(items.Count > 0)
            {
                //remove 1 from index
                items[index].currentStack--;
                //check if stack is 0, set id to -1
                if(items[index].currentStack == 0)
                {
                    items[index].id = -1;
                    items.RemoveAt(index);
                }
                //invoke event
                OnIvnChange?.Invoke();
                return true;
            }
            return false;
        }
        private bool RemoveSingleItem(int itemID)
        {
            return RemoveSingleItem(itemID, -1);
        }
        /// <summary>
        /// returns the index of the first item matched, returns -1 if not found
        /// </summary>
        public int GetFirstItemIndex(int itemID)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (itemID == items[i].id)
                {
                    return i;
                }
            }
            return -1;
        }
        public InventoryItem GetItemAt(int index)
        {
            if(index >= 0 && index < items.Count)
            {
                return items[index];
            }
            return null;
        }
        private void AddItemToNewSlot(int itemID)
        {
            ItemData data = ItemDatabase.GetItemData(itemID);
            InventoryItem itemToAdd = new InventoryItem(data);
            itemToAdd.currentStack = 1;
            items.Add(itemToAdd);
            OnIvnChange?.Invoke();
        }
    }
}