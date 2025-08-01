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

    void Update()
    {

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
            Debug.Log("Weapon name = " + weapon.weaponData.weaponName);
            swapWeaponItem = currentWeaponItem;
            Instantiate(weapon.weaponData.weaponPrefab, weaponPos);
            currentWeaponItem = weapon;
            Player.instance.playerStat.weaponDamage = currentWeaponItem.weaponData.weaponDamage;
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
