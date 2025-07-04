using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    public TMP_Text textCount;
    public Image cooltimeImage;

    private Slot linkedSlot;
    private ItemCooltimeController icc;

    void Awake()
    {
        icc = GameObject.FindWithTag("ICC").GetComponent<ItemCooltimeController>();
    }

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
            cooltimeImage.fillAmount = 0;
        }
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = linkedSlot.item.itemImage;
            itemCount = linkedSlot.itemCount;
            textCount.text = this.itemCount.ToString();

            ItemCooltimeChecker();
        }
    }

    void ItemCooltimeChecker()
    {
        if (linkedSlot.item == null || icc == null)
        {
            cooltimeImage.fillAmount = 0;
        }
        else
        {
            var cooltime = icc.GetCoolTime(linkedSlot.item);
            cooltimeImage.fillAmount = cooltime / linkedSlot.item.cooltime;
        }
    }
}
