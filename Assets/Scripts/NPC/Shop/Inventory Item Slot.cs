using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private TMP_Text countText;

    private Item item;
    private int count;

    public void SetItem(Item _item, int _count)
    {
        item = _item;
        count = _count;
        itemIcon.sprite = item.itemImage;
        itemNameText.text = item.itemName;
        itemPriceText.text = item.sellPrice.ToString();
        countText.text = count.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShopInputNumber.Instance.CallSell(item, item.sellPrice);
        }
    }
}
