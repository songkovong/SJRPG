using TMPro;
using UnityEngine;

public class WeaponDamageTExt : MonoBehaviour
{
    Player player;
    public TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.playerStat.weaponDamage.ToString();
    }
}
