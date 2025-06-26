using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image iconImage;
    public TMP_Text countText;
    public bool onlyUsableItemsAllowed = false;

    public InventoryItemStack currentItem;

    public void SetItem(ItemData itemData, int amount)
    {
        if (onlyUsableItemsAllowed && !itemData.isUsable)
        {
            Debug.Log("Only Usable Item in this slot.");
            return;
        }

        if (currentItem == null) currentItem = new InventoryItemStack();

        currentItem.item = itemData;
        currentItem.count = amount;
        iconImage.sprite = itemData.icon;
        iconImage.enabled = true;

        UpdateUI();
    }

    public void AddItem(int amount)
    {
        if (currentItem != null)
        {
            currentItem.count += amount;
            UpdateUI();
        }
    }

    public void UseItem()
    {
        if (currentItem == null) return;

        currentItem.Use();
        if (currentItem.count <= 0)
            ClearSlot();
        else
            UpdateUI();
    }

    public void ClearSlot()
    {
        currentItem = null;
        iconImage.enabled = false;
        countText.text = "";
    }

    public void UpdateUI()
    {
        countText.text = currentItem.count.ToString();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dragged == null) return;

        if (onlyUsableItemsAllowed && !dragged.itemData.isUsable)
        {
            Debug.Log("Only Usable Item in this slot.");
            return;
        }

        // overlap
        if (currentItem != null && dragged.itemData == currentItem.item)
        {
            AddItem(dragged.itemCount);
            Destroy(dragged.gameObject);
        }
        else // swap
        {
            var prevSlot = dragged.parentSlot;
            var temp = currentItem;

            currentItem = prevSlot.currentItem;
            prevSlot.currentItem = temp;

            UpdateUI();
            prevSlot.UpdateUI();
        }        
    }
}
