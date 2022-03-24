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
    public static GameObject player;
    private Inventory inventory;
    private PlayerInfo info;
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
        slots = new Slot[info.inventorySize.GetValue()];
        DestroyChildren();
        for (int i = 0; i < info.inventorySize.GetValue(); i++)
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
        player = (GameObject.FindGameObjectsWithTag("Player"))[0];
        info = player.GetComponent<PlayerInfo>();
        inventory = player.GetComponent<Inventory>();

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

