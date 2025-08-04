using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private TMP_Text countText;

    private SlotTooltip tooltip;

    private Item item;
    private int count;

    void Start()
    {
        tooltip = GameObject.FindWithTag("Item Tooltip").GetComponent<SlotTooltip>();
    }

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
