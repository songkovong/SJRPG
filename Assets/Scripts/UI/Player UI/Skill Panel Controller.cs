using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelController : MonoBehaviour
{
    private Player player;

    [Header("Skill Texts")]
    public TMP_Text skill1Text;
    public TMP_Text skill2Text;
    public TMP_Text skill3Text;
    public TMP_Text skill4Text;
    public TMP_Text skill5Text;
    public TMP_Text skill6Text;

    [Header("Skill Up Buttons")]
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;
    public Button skill4Button;
    public Button skill5Button;
    public Button skill6Button;

    [Header("Skill Panel Exit Button")]
    public Button skillPanelExitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.instance;

        if (player == null)
        {
            Debug.Log("Player Found Error");
        }

        skill1Button.onClick.AddListener(Skill1Up);
        skill2Button.onClick.AddListener(Skill2Up);
        skill3Button.onClick.AddListener(Skill3Up);
        skill4Button.onClick.AddListener(Skill4Up);
        skill5Button.onClick.AddListener(Skill5Up);
        skill6Button.onClick.AddListener(Skill6Up);

        skillPanelExitButton.onClick.AddListener(PanelExit);

    }

    void OnEnable()
    {
        UpdatePanel();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (player == null || player.playerStat == null || player.playerStat.data == null)
    //     {
    //         return;
    //     }

    //     skill1Text.text = player.playerStat.spaceSkill.level.ToString();
    //     skill2Text.text = player.playerStat.cSkill.level.ToString();
    //     skill3Text.text = player.playerStat.rSkill.level.ToString();
    //     skill4Text.text = player.playerStat.attackMastery.level.ToString();
    //     skill5Text.text = player.playerStat.guardSkill.level.ToString();
    //     skill6Text.text = player.playerStat.comboAttackSkill.level.ToString();
    // }

    void UpdatePanel()
    {
        if (player == null || player.playerStat == null || player.playerStat.data == null)
        {
            return;
        }

        skill1Text.text = player.playerStat.spaceSkill.level.ToString();
        skill2Text.text = player.playerStat.cSkill.level.ToString();
        skill3Text.text = player.playerStat.rSkill.level.ToString();
        skill4Text.text = player.playerStat.attackMastery.level.ToString();
        skill5Text.text = player.playerStat.guardSkill.level.ToString();
        skill6Text.text = player.playerStat.comboAttackSkill.level.ToString();
    }

    void Skill1Up()
    {
        player.playerStat.SpaceSkillUp();
        UpdatePanel();
    }

    void Skill2Up()
    {
        player.playerStat.CSkillUp();
        UpdatePanel();
    }

    void Skill3Up()
    {
        player.playerStat.RSkillUp();
        UpdatePanel();
    }

    void Skill4Up()
    {
        player.playerStat.AtkMasterUp();
        UpdatePanel();
    }

    void Skill5Up()
    {
        player.playerStat.GuardUp();
        UpdatePanel();
    }

    void Skill6Up()
    {
        player.playerStat.ComboAttackUp();
        UpdatePanel();
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }
}
