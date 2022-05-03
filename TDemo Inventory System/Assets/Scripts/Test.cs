using System.Collections;
using System.Collections.Generic;
using Timber.InventorySystem;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Inventory inv;
    [SerializeField] private InventoryUI invUI;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            inv.AddItem(1, 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            inv.AddItem(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inv.RemoveItemAt(invUI.GetCurserHover(), 1);
        }
    }
}
