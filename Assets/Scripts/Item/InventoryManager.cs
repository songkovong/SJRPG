using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            slots[i].onlyUsableItemsAllowed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + 1))
            {
                slots[i].UseItem();
            }
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        if (item.isUsable)
        {
            for (int i = 0; i < 4; i++)
            {
                if (slots[i].currentItem != null && slots[i].currentItem.item == item)
                {
                    slots[i].AddItem(amount);
                    return true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (slots[i].currentItem == null)
                {
                    slots[i].SetItem(item, amount);
                    return true;
                }
            }
        }

        for (int i = 4; i < slots.Length; i++)
        {
            if (slots[i].currentItem != null && slots[i].currentItem.item == item)
            {
                slots[i].AddItem(amount);
                return true;
            }
        }

        for (int i = 4; i < slots.Length; i++)
        {
            if (slots[i].currentItem == null)
            {
                slots[i].SetItem(item, amount);
                return true;
            }
        }

        return false; // full inventory.
    }
}
