using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private ShopUI shopUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        shopPanel.SetActive(false);
    }

    public void OpenShop(string shopID)
    {
        ShopData shopData = FindShopByID(shopID);

        if (shopData == null)
        {
            return;
        }

        // shopPanel.SetActive(true);
        shopUI.SetShop(shopData);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void RefreshPlayerItem()
    {
        shopUI.RefreshPlayerItems();
    }

    private ShopData FindShopByID(string id)
    {
        ShopData[] shops = Resources.LoadAll<ShopData>("Shops");
        foreach (var shop in shops)
        {
            if (shop.shopID == id) return shop;
        }

        return null;
    }
}
