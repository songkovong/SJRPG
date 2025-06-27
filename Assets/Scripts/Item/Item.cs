using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        QuestItem,
        ETC
    }

    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public float itemDropRate;
    public GameObject itemPrefab;
}
