using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public Transform weaponPos;

    public Item currentWeaponItem;
    public Item swapWeaponItem;

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

            SaveWeapon();
        }
    }

    void SaveWeapon()
    {
        PlayerPrefs.SetInt("Weapon Code", currentWeaponItem.weaponData.weaponCode);
    }

    Item LoadWeapon()
    {
        if (PlayerPrefs.HasKey("Weapon Code"))
        {
            var weaponCode = PlayerPrefs.GetInt("Weapon Code");
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
}
