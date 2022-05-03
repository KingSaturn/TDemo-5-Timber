using System.Collections;
using System.Collections.Generic;
using Timber.InventorySystem;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private TMP_Text deadCurrency;
    [SerializeField] private TMP_Text oakCurrency;
    private const int k_OakLogID = 1;
    private const int k_DeadLogID = 2;
    private const int k_AxeID = 3;
    private Inventory inv;
    private Inventory GetInventory()
    {
        if(inv == null)
        {
            inv = FindObjectOfType<Inventory>();
        }
        return inv;
    }
    public int DeadLogCount()
    {
        return GetInventory().GetItemCount(k_DeadLogID);
    }
    public bool AxeCheck()
    {
        if(GetInventory().GetItemCount(k_AxeID) >= 1)
        {
            return true;
        }
        return false;
    }
    public int OakLogCount()
    {
        return GetInventory().GetItemCount(k_OakLogID);
    }
    private void Update()
    {
        if (OakLogCount() != 0 || DeadLogCount() != 0)
        {
            deadCurrency.text = DeadLogCount().ToString();
            oakCurrency.text = OakLogCount().ToString();
        }
    }
    public bool PurchaseItem(int shopItemID, int oakLogCost, int deadLogCost)
    {
        if(shopItemID == k_AxeID && AxeCheck() == true)
        {
            return false;
        }
        else if (OakLogCount() >= oakLogCost && DeadLogCount() >= deadLogCost)
        {
            GetInventory().RemoveItem(k_OakLogID, oakLogCost);
            GetInventory().RemoveItem(k_DeadLogID, deadLogCost);
            GetInventory().AddItem(shopItemID,1);
            return true;
        }
        return false;
    }
}
