using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] ShopController shopCont;
    [SerializeField] private TMP_Text deadText;
    [SerializeField] private TMP_Text oakText;
    [SerializeField] private int oakCost;
    [SerializeField] private int deadCost;
    [SerializeField] private int shopItemID;
    private void OnValidate()
    {
        if (shopCont == null)
            return;
        if(oakText != null)
        {
            oakText.text = oakCost.ToString();
            oakText.gameObject.SetActive(oakCost > 0);
        }
        if(deadText != null)
        {
            deadText.text = deadCost.ToString();
            deadText.gameObject.SetActive(deadCost>0);
        }
    }
    public void Purchase()
    {
        shopCont.PurchaseItem(shopItemID, oakCost, deadCost);
    }
}
