using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public bool isUsable;

    public virtual void Use()
    {
        if (isUsable)
        {
            Debug.Log($"Using {itemName})");
        }
    }
}
