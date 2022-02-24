using Timber.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text stackCount;
    private InventoryUI invUI;
    private InventoryItem item;
    private int slotIndex;
    public void Init(InventoryUI parent, int index)
    {
        slotIndex = index;
        invUI = parent;
    }
    public void SetItem(InventoryItem newItem)
    {
        item = newItem;
        if(item == null)
        {
            SetText("");
            SetImage(null);
            return;
        }
        SetText(item.currentStack.ToString());
        SetImage(item.icon);
    }
    public InventoryItem GetItem()
    {
        return item;
    }
    public void SetText(string text)
    {
        stackCount.text = text;
    }
    public void SetImage(Sprite image)
    {
        itemSprite.sprite = image;

        /*if (image == null)
        {
            itemSprite.enabled = false;
        }
        else
        {
            itemSprite.enabled = true;
        }
        */
        itemSprite.enabled = image != null;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        invUI.SetCurserSlot(slotIndex);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        invUI.SetCurserSlot(-1);
    }
}
