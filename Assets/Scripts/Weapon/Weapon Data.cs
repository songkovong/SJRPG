using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon/Weapon Data", fileName = "New Weapon Data")]
public class WeaponData : ScriptableObject
{
    // Weapon Code is Same itemCode
    public int weaponCode;
    public string weaponName;
    public GameObject weaponPrefab;
    public float weaponDamage;
    public float weaponSpeed;
}
