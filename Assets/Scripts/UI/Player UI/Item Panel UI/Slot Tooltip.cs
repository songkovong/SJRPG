using TMPro;
using UnityEngine;

public class SlotTooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipPanel;

    [SerializeField]
    private TMP_Text text_ItemName;
    [SerializeField]
    private TMP_Text text_ItemDesc;
    [SerializeField]
    private TMP_Text text_ItemHowtoUse;

    public void ShowTooltip(Item _item, Vector3 _pos)
    {
        tooltipPanel.SetActive(true);

        _pos += new Vector3(tooltipPanel.GetComponent<RectTransform>().rect.width * 0.2f,
            tooltipPanel.GetComponent<RectTransform>().rect.height * 0.01f,
            0);
        tooltipPanel.transform.position = _pos;

        text_ItemName.text = _item.itemName;
        text_ItemDesc.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
            text_ItemHowtoUse.text = "Mouse Right Click - Equip";
        else if (_item.itemType == Item.ItemType.Consumable)
            text_ItemHowtoUse.text = "Mouse Right Click - Consume";
        else
            text_ItemHowtoUse.text = "";
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
