using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text priceText;

    private Item item;
    private int price;

    public void SetItem(Item _item)
    {
        item = _item;
        price = _item.buyPrice;
        itemIcon.sprite = item.itemImage;
        itemNameText.text = item.itemName;
        priceText.text = price.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShopInputNumber.Instance.CallBuy(item, price);
        }
    }
}
