using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    Player player;
    ShopInputNumber _inputNumber;

    [SerializeField] private Transform shopItemParent;
    [SerializeField] private GameObject shopItemSlotPrefab;

    [SerializeField] private Transform playerItemParent;
    [SerializeField] private GameObject playerItemSlotPrefab;

    [Header("Buttons")]
    public Button ExitButton;
    public Button OkButton;
    public Button CancelButton;

    void Start()
    {
        player = Player.instance;

        if (player == null)
        {
            Debug.Log("Player Found Error");
        }

        _inputNumber = ShopInputNumber.Instance;

        ExitButton.onClick.AddListener(PanelExit);
        OkButton.onClick.AddListener(Ok);
        CancelButton.onClick.AddListener(Cancel);
    }

    public void SetShop(ShopData shopData)
    {
        RefreshShopItems(shopData);
        RefreshPlayerItems();
    }

    public void RefreshShopItems(ShopData shopData)
    {
        foreach (Transform child in shopItemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in shopData.sellItems)
        {
            GameObject slotObj = Instantiate(shopItemSlotPrefab, shopItemParent);
            ShopItemSlot slot = slotObj.GetComponent<ShopItemSlot>();
            slot.SetItem(item);
        }
    }

    public void RefreshPlayerItems()
    {
        foreach (Transform child in playerItemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var slot in Player.instance.inventory.slots)
        {
            if (slot.item != null && slot.itemCount > 0)
            {
                GameObject slotObj = Instantiate(playerItemSlotPrefab, playerItemParent);
                InventoryItemSlot slotUI = slotObj.GetComponent<InventoryItemSlot>();
                slotUI.SetItem(slot.item, slot.itemCount);
            }
        }
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }

    void Ok()
    {
        _inputNumber.OK();
        SoundManager.Instance.Play2DSound("Buy Sell Sound");
    }

    void Cancel()
    {
        _inputNumber.Cancel();
    }
}
