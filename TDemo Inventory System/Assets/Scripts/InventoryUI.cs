using System;
using System.Collections;
using System.Collections.Generic;
using Timber.InventorySystem;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Slot[] slots;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private Inventory inventory;
    private int curserHover = -1;
    public int GetCurserHover()
    {
        return curserHover;
    }
    public void SetCurserSlot(int value)
    {
        curserHover = value;
    }
    public void GenerateEmptySlots()
    {
        slots = new Slot[inventorySize];
        DestroyChildren();
        for (int i = 0; i < inventorySize; i++)
        {
            Slot s = Instantiate(slotPrefab, slotsParent);
            s.Init(this, i);
            s.SetImage(null);
            s.SetText("");
            slots[i] = s;
        }
       
    }
    public void UpdateUI()
    {
        for (int i = 0; i < inventory.maxInventorySize; i++)
        {
            InventoryItem item = inventory.GetItemAt(i);
            if(item == null)
            {
                slots[i].SetItem(null);
                continue;
            }
            slots[i].SetItem(item);
        }
    }
    public void Start()
    {
        inventory.OnIvnChange += OnInvChange;
        GenerateEmptySlots();
    }

    private void OnInvChange()
    {
        UpdateUI();
    }

    public void DestroyChildren()
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            Destroy(slotsParent.GetChild(i).gameObject);
        }
    }
}

