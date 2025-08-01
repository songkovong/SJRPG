using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPanelController : MonoBehaviour
{
    private Player player;

    [Header("Current Player Data")]
    public TMP_Text curLevel;
    public TMP_Text curExp;
    public TMP_Text curHealth;
    public TMP_Text curMagic;
    public TMP_Text curAttack;
    public TMP_Text curSkillDamage;
    public TMP_Text curAttackSpeed;
    public TMP_Text curSpeed;
    public TMP_Text curDepend;

    [Header("Current Player Stat and Point")]
    public TMP_Text curStatPoint;
    public TMP_Text curStrengthPoint;
    public TMP_Text curAgilityPoint;
    public TMP_Text curMagicPoint;
    public TMP_Text curDependPoint;

    [Header("Stat Point Up Buttons")]
    public Button strengthUpButton;
    public Button agilityUpButton;
    public Button magicUpButton;
    public Button dependUpButton;

    [Header("Panel Exit Button")]
    public Button panelExitButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.instance;
        
        if (player == null)
        {
            Debug.Log("Player Found Error");
        }

        strengthUpButton.onClick.AddListener(StrengthUp);
        agilityUpButton.onClick.AddListener(AgilityUp);
        magicUpButton.onClick.AddListener(MagicUp);
        dependUpButton.onClick.AddListener(DependUp);

        panelExitButton.onClick.AddListener(PanelExit);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || player.playerStat == null || player.playerStat.data == null)
        {
            return;
        }
        var damage = player.playerStat.data.attackDamage;
        var weaponDamage = player.playerStat.weaponDamage;
        var mastery = player.playerStat.attackMastery.masteryStat;
        var minDamage = (damage + weaponDamage) * (0.1f + mastery) + 1;
        var maxDamage = damage + weaponDamage;
        var skillDamage = player.playerStat.data.magic;

        curLevel.text = player.playerStat.data.level.ToString();
        curExp.text = Mathf.FloorToInt(player.playerStat.data.expCount).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.exp).ToString();
        curHealth.text = Mathf.FloorToInt(player.playerStat.data.currentHealth).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxHealth).ToString();
        curMagic.text = Mathf.FloorToInt(player.playerStat.data.currentMagic).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxMagic).ToString();
        curAttack.text = Mathf.FloorToInt(minDamage).ToString() + " ~ " + Mathf.FloorToInt(maxDamage).ToString();
        curSkillDamage.text = Mathf.FloorToInt(minDamage + skillDamage).ToString() + " ~ " + Mathf.FloorToInt(maxDamage + skillDamage).ToString();
        curAttackSpeed.text = player.playerStat.data.attackSpeed.ToString("F2");
        curSpeed.text = player.playerStat.data.moveSpeed.ToString("F2") + " ~ " + player.playerStat.data.sprintSpeed.ToString("F2");
        curDepend.text = player.playerStat.data.dependRate.ToString();

        curStatPoint.text = player.playerStat.data.statPoint.ToString() + " SP";
        curStrengthPoint.text = player.playerStat.data.strength.ToString();
        curAgilityPoint.text = player.playerStat.data.agility.ToString();
        curMagicPoint.text = player.playerStat.data.magic.ToString();
        curDependPoint.text = player.playerStat.data.depend.ToString();

    }

    void StrengthUp()
    {
        player.playerStat.StrengthUp();
    }

    void AgilityUp()
    {
        player.playerStat.AgilityUp();
    }

    void MagicUp()
    {
        player.playerStat.MagicUp();
    }

    void DependUp()
    {
        player.playerStat.DependUp();
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }
}
