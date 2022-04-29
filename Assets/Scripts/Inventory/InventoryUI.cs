using System;
using System.Collections;
using System.Collections.Generic;
using Timber.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Slot[] slots;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Slot slotPrefab;
    public static GameObject player;
    public Inventory inventory;
    private int swappedItem;
    private bool isSwapping;
    private PlayerInfo info;
    private int curserHover = -1;
    private void Awake()
    {
        player = (GameObject.FindGameObjectsWithTag("Player"))[0];
        info = player.GetComponent<PlayerInfo>();
        inventory = player.GetComponent<Inventory>();
        isSwapping = false;

        inventory.OnIvnChange += OnInvChange;
        GenerateEmptySlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GetCurserHover() < inventory.items.Count && GetCurserHover() >= 0)
            {
                swappedItem = GetCurserHover();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (GetCurserHover() < inventory.items.Count && GetCurserHover() >= 0)
            {
                inventory.SwapItems(GetCurserHover(), swappedItem);
            }
            swappedItem = -1;
        }

    }

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

