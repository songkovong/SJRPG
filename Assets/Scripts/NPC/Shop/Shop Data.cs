using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Data", menuName = "Shop/New Shop Data")]
public class ShopData : ScriptableObject
{
    public string shopID;
    public Item[] sellItems;
}
