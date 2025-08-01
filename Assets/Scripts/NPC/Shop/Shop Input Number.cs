using TMPro;
using UnityEngine;

public class ShopInputNumber : MonoBehaviour
{
    public static ShopInputNumber Instance { get; private set; }

    [SerializeField] private GameObject go_Base;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text previewText;
    [SerializeField] private ShopUI shopUI;

    private Item currentItem;
    private int currentPrice;
    private bool isBuying = false;
    private bool active = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        shopUI = GetComponentInParent<ShopUI>();
    }

    void Update()
    {
        if (!active) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OK();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    public void CallBuy(Item _item, int price)
    {
        currentItem = _item;
        currentPrice = price;
        isBuying = true;

        previewText.text = "1";
        inputField.text = "";
        go_Base.SetActive(true);
        active = true;
    }

    public void CallSell(Item _item, int price)
    {
        currentItem = _item;
        currentPrice = price;
        isBuying = false;

        previewText.text = "1";
        inputField.text = "";
        go_Base.SetActive(true);
        active = true;
    }

    public void Cancel()
    {
        go_Base.SetActive(false);
        active = false;
        currentItem = null;
    }

    public void OK()
    {
        int num = 1;

        if (!string.IsNullOrEmpty(inputField.text) && int.TryParse(inputField.text, out int parsed))
        {
            if (parsed > 0) num = parsed;
        }

        if (isBuying)
        {
            int totalPrice = currentPrice * num;
            if (Player.instance.inventory.SpendCoin(totalPrice))
            {
                if (currentItem.itemType == Item.ItemType.Equipment)
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (Player.instance.inventory.IsFull())
                        {
                            Player.instance.inventory.AcquireCoin(currentPrice);
                            continue;
                        }
                        Player.instance.inventory.AcquireItem(currentItem);
                    }
                }
                else
                {
                    if (!Player.instance.inventory.IsFull())
                    {
                        Player.instance.inventory.AcquireItem(currentItem, num);
                    }
                    else
                    {
                        if (Player.instance.inventory.CanStack(currentItem))
                        {
                            Player.instance.inventory.AcquireItem(currentItem, num);
                        }
                        Player.instance.inventory.AcquireCoin(totalPrice);
                    }
                }
            }
        }
        else
        {
            Player.instance.inventory.SellItem(currentItem, num);
        }

        shopUI.RefreshPlayerItems();

        go_Base.SetActive(false);
        active = false;
    }
}
