using UnityEngine;

[System.Serializable]
public class InventoryItemStack : MonoBehaviour
{
    public ItemData item;
    public int count;

    public void Add(int amount) => count += amount;

    public void Use()
    {
        if (item.isUsable)
        {
            item.Use();
            count--;
        }
    }
}
