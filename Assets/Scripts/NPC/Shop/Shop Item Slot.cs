using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text priceText;
    private SlotTooltip tooltip;

    private Item item;
    private int price;

    void Start()
    {
        tooltip = GameObject.FindWithTag("Item Tooltip").GetComponent<SlotTooltip>();
    }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Vector3 mousePos = Input.mousePosition;
            tooltip.ShowTooltip(item, mousePos);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
