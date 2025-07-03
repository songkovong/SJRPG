using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    public TMP_Text textCount;

    private Slot linkedSlot;

    public void LinkToSlot(Slot slot)
    {
        linkedSlot = slot;
    }

    void Update()
    {
        if (linkedSlot == null || linkedSlot.item == null)
        {
            itemImage.enabled = false;
            itemCount = 0;
            textCount.text = "";
        }
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = linkedSlot.item.itemImage;
            itemCount = linkedSlot.itemCount;
            textCount.text = this.itemCount.ToString();
        }
    }
}
