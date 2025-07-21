using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    // [SerializeField]
    private GameObject slotsParent;
    // [SerializeField]
    private GameObject coinObject;

    public Slot[] slots { get; private set; }
    public PlayerCoin coin { get; private set; }

    // [SerializeField]
    private GameObject quickSlotParent;
    private QuickSlot[] quickSlots;

    public ItemDatabase idb { get; private set; }


    void Awake()
    {
        slotsParent = GameObject.Find("Slot Panel");
        coinObject = GameObject.Find("Coin Panel");
        quickSlotParent = GameObject.Find("QuickSlot Object");

        if (slotsParent == null) Debug.Log("Slot Parent is null");
        else Debug.Log(slotsParent.name);

        if (coinObject == null) Debug.Log("Coin Parent is null");
        else Debug.Log(coinObject.name);

        if (quickSlotParent == null) Debug.Log("QuickSlot Parent is null");
        else Debug.Log(quickSlotParent.name);

        coin = coinObject.GetComponentInChildren<PlayerCoin>(true);
    }

    void Start()
    {
        idb = GameManager.instance.gameObject.GetComponent<ItemDatabase>();

        InitializeSlot();
        InitializeQuickSlot();
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    public void AcquireCoin(int _amount)
    {
        coin.AddCoin(_amount);
        coin.SetCoinCount();
    }

    public void UseSlotItem(int _idx)
    {
        var idx = _idx - 1;

        if (idx >= 0 && idx <= 3 && idx < slots.Length && slots[idx] != null)
        {
            slots[_idx - 1].UseSlotItem();
        }
    }

    void InitializeSlot()
    {
        slots = new Slot[slotsParent.transform.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsParent.transform.GetChild(i).GetComponent<Slot>();
        }
    }

    void InitializeQuickSlot()
    {
        quickSlots = new QuickSlot[4];

        for (int i = 0; i < 4; i++)
        {
            quickSlots[i] = quickSlotParent.transform.GetChild(i).GetComponent<QuickSlot>();
            quickSlots[i].LinkToSlot(slots[i]);
        }
    }

    public void SaveSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                var itemName = slots[i].item.itemName;
                var itemCount = slots[i].itemCount;

                PlayerPrefs.SetString("Slot" + i + "Name", itemName);
                PlayerPrefs.SetInt("Slot" + i + "Count", itemCount);
            }

            else
            {
                PlayerPrefs.DeleteKey("Slot" + i + "Name");
                PlayerPrefs.DeleteKey("Slot" + i + "Count");
            }
        }
        PlayerPrefs.Save();
    }

    public void LoadSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (PlayerPrefs.HasKey("Slot" + i + "Name"))
            {
                var itemName = PlayerPrefs.GetString("Slot" + i + "Name");
                var itemCount = PlayerPrefs.GetInt("Slot" + i + "Count");

                Item item = idb.GetItemByName(itemName);

                if (item != null)
                {
                    slots[i].AddItem(item, itemCount);
                }
            }
        }
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin.currentCoin);
    }

    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin.AddCoin(
                PlayerPrefs.GetInt("Coin")
            );
            coin.SetCoinCount();
        }
    }
}
