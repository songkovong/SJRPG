using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    public Image cooltimeImage;
    private RectTransform baseRectTransform;
    Player player;
    InputNumber _inputNumber;
    ItemEffectDataBase db;
    ItemCooltimeController icc;

    [SerializeField]
    private TMP_Text textCount;

    void Awake()
    {
        baseRectTransform = transform.parent.GetComponent<RectTransform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _inputNumber = GameObject.FindWithTag("Input Number").GetComponent<InputNumber>();
        db = GameObject.FindWithTag("DB").GetComponent<ItemEffectDataBase>();
        icc = GameObject.FindWithTag("ICC").GetComponent<ItemCooltimeController>();
    }

    void Update()
    {
        ItemCooltimeChecker();
    }

    // Set Item Color and Alpha
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // Add Item in Slot
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            textCount.text = itemCount.ToString();
        }
        else
        {
            textCount.text = " ";
        }

        SetColor(1);
    }

    // Update Slot Count
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        textCount.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // Delete this Slot
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        textCount.text = " ";
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
    
    void ItemUseBase()
    {
        if (item != null && icc.CanUseItem(item))
        {
            db.UseItem(item);
            icc.UseItem(item);

            if (item.itemType == Item.ItemType.Consumable)
            {
                SetSlotCount(-1);
            }

            else if (item.itemType == Item.ItemType.Equipment)
            {
                // WeaponManager.Instance.EquipWeapon(item);
                SetSlotCount(-1);
                var newWeapon = WeaponManager.Instance.swapWeaponItem;
                if (newWeapon != null)
                {
                    AddItem(newWeapon);
                }
            }

            ShopManager.Instance.RefreshPlayerItem();
        }
    }

    public void UseSlotItem()
    {
        ItemUseBase();
    }

    void ItemCooltimeChecker()
    {
        if (item == null)
        {
            cooltimeImage.fillAmount = 0;
        }
        else
        {
            var cooltime = icc.GetCoolTime(item);
            cooltimeImage.fillAmount = cooltime / item.cooltime;
        }
    }

    

    // Mouse Right button Click to Use Item or Equip.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ItemUseBase();
        }
    }

    // Begin Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // Draging..
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // End Drag
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 localPosition;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            baseRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPosition
        );

        if (isInside && baseRectTransform.rect.Contains(localPosition))
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
        else
        {
            if (DragSlot.instance.dragSlot != null)
                _inputNumber.Call();
        }
    }

    // Drop Item
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    // Pointer Enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            db.ShowTooltip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        db.HideTooltip();
    }
}
