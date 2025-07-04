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
    [TextArea] // 여러줄 가능
    public string itemDesc;
    public ItemType itemType;
    public Sprite itemImage;
    public float itemDropRate;
    public float cooltime;
    public GameObject itemPrefab;
}
