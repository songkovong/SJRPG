// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class HUDController : MonoBehaviour
// {
//     Image healthImage, magicImage, expImage, skillImage;
//     TMP_Text healthText, magicText, expText, skillText;

// }

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    private Player player; 

    [Header("체력 UI")]
    public Image healthFillImage; 
    public TMP_Text healthText;   

    [Header("마나 UI")]
    public Image magicFillImage;  
    public TMP_Text magicText;    

    [Header("경험치 UI")]
    public Image expFillImage;    
    public TMP_Text expText;      

    [Header("스킬 UI")]
    public Image spaceSkillImage; 
    public TMP_Text spaceSkillText;
    public Image cSkillImage;     
    public TMP_Text cSkillText;   
    public Image rSkillImage;     
    public TMP_Text rSkillText;   

    void Start()
    {
        player = Player.instance;

        if (player == null)
        {
            Debug.LogError("오류: Player 싱글톤 인스턴스를 찾을 수 없습니다. Player 스크립트가 로드되었는지 확인하세요.");
        }
    }

    void Update()
    {
        if (player == null || player.playerStat == null || player.playerStat.data == null)
        {
            return;
        }

        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = player.playerStat.data.currentHealth / player.playerStat.data.maxHealth;
        }
        if (healthText != null)
        {
            healthText.text = Mathf.FloorToInt(player.playerStat.data.currentHealth).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxHealth).ToString();
        }

        if (magicFillImage != null)
        {
            magicFillImage.fillAmount = player.playerStat.data.currentMagic / player.playerStat.data.maxMagic;
        }
        if (magicText != null)
        {
            magicText.text = Mathf.FloorToInt(player.playerStat.data.currentMagic).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxMagic).ToString();
        }

        if (expFillImage != null)
        {
            expFillImage.fillAmount = player.playerStat.data.expCount / player.playerStat.data.exp;
        }
        if (expText != null)
        {
            expText.text = Mathf.FloorToInt(player.playerStat.data.expCount).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.exp).ToString();
        }

        if (player.playerStat.spaceSkill != null)
        {
            if (spaceSkillImage != null)
            {
                spaceSkillImage.color = player.playerStat.spaceSkill.IsLevel0() ? Color.gray : Color.white;
                spaceSkillImage.fillAmount = player.playerStat.spaceSkill.timer / player.playerStat.spaceSkill.cooltime;
            }
            if (spaceSkillText != null)
            {
                float remainingCooltime = player.playerStat.spaceSkill.cooltime - player.playerStat.spaceSkill.timer;
                spaceSkillText.text = (remainingCooltime <= 0) ? " " : Mathf.FloorToInt(remainingCooltime).ToString();
            }
        }

        if (player.playerStat.cSkill != null)
        {
            if (cSkillImage != null)
            {
                cSkillImage.color = player.playerStat.cSkill.IsLevel0() ? Color.gray : Color.white;
                cSkillImage.fillAmount = player.playerStat.cSkill.timer / player.playerStat.cSkill.cooltime;
            }
            if (cSkillText != null)
            {
                float remainingCooltime = player.playerStat.cSkill.cooltime - player.playerStat.cSkill.timer;
                cSkillText.text = (remainingCooltime <= 0) ? " " : Mathf.FloorToInt(remainingCooltime).ToString();
            }
        }

        if (player.playerStat.rSkill != null)
        {
            if (rSkillImage != null)
            {
                rSkillImage.color = player.playerStat.rSkill.IsLevel0() ? Color.gray : Color.white;
                rSkillImage.fillAmount = player.playerStat.rSkill.timer / player.playerStat.rSkill.cooltime;
            }
            if (rSkillText != null)
            {
                float remainingCooltime = player.playerStat.rSkill.cooltime - player.playerStat.rSkill.timer;
                rSkillText.text = (remainingCooltime <= 0) ? " " : Mathf.FloorToInt(remainingCooltime).ToString();
            }
        }
    }
}