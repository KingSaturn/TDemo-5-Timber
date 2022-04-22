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
            int rnd = Random.Range(1, 5);
            while(rnd == 3 && inv.GetFirstItemIndex(3) != -1)
            {
                rnd = Random.Range(1, 5);
            }
            inv.AddItem(rnd, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inv.RemoveItemAt(invUI.GetCurserHover(), 1);
        }
    }
}
