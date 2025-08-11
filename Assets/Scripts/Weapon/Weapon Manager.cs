using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public Transform weaponPos;

    public Item currentWeaponItem;
    public Item swapWeaponItem;

    public Image weaponImage;
    public TMP_Text weaponNameText;
    public TMP_Text weaponDescText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentWeaponItem = LoadWeapon();
        if (currentWeaponItem != null)
        {
            EquipWeapon(currentWeaponItem);
        }
    }
    public void EquipWeapon(Item weapon)
    {
        if (weapon != null && weapon.weaponData != null)
        {
            // Equip
            if (weaponPos.childCount > 0)
            {
                Destroy(weaponPos.GetChild(0).gameObject); // current weapon destroy;
            }

            swapWeaponItem = currentWeaponItem;

            GameObject newWeaponObj = Instantiate(weapon.weaponData.weaponPrefab, weaponPos);
            currentWeaponItem = weapon;

            Player.instance.playerStat.weaponDamage = currentWeaponItem.weaponData.weaponDamage;
            Player.instance.playerStat.weaponSpeed = currentWeaponItem.weaponData.weaponSpeed;

            SwordHitbox hitbox = newWeaponObj.GetComponentInChildren<SwordHitbox>();
            if (hitbox != null)
            {
                Player.instance.SetWeaponHitbox(hitbox);
            }

            PlayerTrail trail = newWeaponObj.GetComponentInChildren<PlayerTrail>();
            if (trail != null)
            {
                Player.instance.SetWeaponTrail(trail);
            }

            SetWeaponImageAndText();

            SaveWeapon();

            SoundManager.Instance.Play2DSound("Equip Sound");
        }
    }

    public void TakeOffWeapon()
    {
        if (weaponPos.childCount > 0)
        {
            if (Player.instance.inventory.AcquireItem(currentWeaponItem))
            {
                Destroy(weaponPos.GetChild(0).gameObject); // current weapon destroy;
                Player.instance.playerStat.weaponDamage = 1f;
                Player.instance.playerStat.weaponSpeed = 1f;

                currentWeaponItem = null;

                SetWeaponImageAndText();

                SoundManager.Instance.Play2DSound("Unequip Sound");
            }
        }
    }

    void SaveWeapon()
    {
        PlayerPrefs.SetInt("Weapon Code", currentWeaponItem.weaponData.weaponCode);
    }

    Item LoadWeapon()
    {
        var weaponCode = PlayerPrefs.HasKey("Weapon Code") ? PlayerPrefs.GetInt("Weapon Code") : 101;
        // if (PlayerPrefs.HasKey("Weapon Code"))
        {
            // var weaponCode = PlayerPrefs.GetInt("Weapon Code");
            Item[] items = Resources.LoadAll<Item>("Items");
            foreach (Item item in items)
            {
                if (item.weaponData != null && item.weaponData.weaponCode == weaponCode)
                {
                    return item;
                }
            }
        }
        return null;
    }

    void SetWeaponImageAndText()
    {
        if (currentWeaponItem != null)
        {
            weaponImage.sprite = currentWeaponItem.itemImage;
            weaponNameText.text = currentWeaponItem.itemName;
            weaponDescText.text = currentWeaponItem.itemDesc + $"\n(Attack: {currentWeaponItem.weaponData.weaponDamage}, Speed: {currentWeaponItem.weaponData.weaponSpeed})";
        }
        else
        {
            weaponImage.sprite = null;
            weaponNameText.text = "No Weapon";
            weaponDescText.text = "No Weapon.";
        }
    }
}
