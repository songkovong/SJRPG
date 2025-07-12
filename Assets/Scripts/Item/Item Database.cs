using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public Item GetItemByName(string name)
    {
        return items.Find(x => x.itemName == name);
    }
}
