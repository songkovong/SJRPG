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
    // 플레이어 스크립트 참조
    // 싱글톤이므로 public으로 할당할 필요가 없지만, 인스펙터에서 확인용으로 남겨둘 수도 있습니다.
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
        // Player 싱글톤 인스턴스를 가져옵니다.
        // Player 스크립트 내부에 Instance 프로퍼티가 있어야 합니다.
        player = Player.instance;

        if (player == null)
        {
            Debug.LogError("오류: Player 싱글톤 인스턴스를 찾을 수 없습니다. Player 스크립트가 로드되었는지 확인하세요.");
        }

        // UI 요소 할당은 Inspector에서 직접 드래그 앤 드롭하는 것이 가장 좋습니다.
        // 이 부분은 기존과 동일합니다.
    }

    void Update()
    {
        // 플레이어 참조 또는 데이터가 없는 경우 업데이트를 건너뜀
        if (player == null || player.playerStat == null || player.playerStat.data == null)
        {
            return;
        }

        // 체력 UI 업데이트
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = player.playerStat.data.currentHealth / player.playerStat.data.maxHealth;
        }
        if (healthText != null)
        {
            healthText.text = Mathf.FloorToInt(player.playerStat.data.currentHealth).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxHealth).ToString();
        }

        // 마나 UI 업데이트
        if (magicFillImage != null)
        {
            magicFillImage.fillAmount = player.playerStat.data.currentMagic / player.playerStat.data.maxMagic;
        }
        if (magicText != null)
        {
            magicText.text = Mathf.FloorToInt(player.playerStat.data.currentMagic).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.maxMagic).ToString();
        }

        // 경험치 UI 업데이트
        if (expFillImage != null)
        {
            expFillImage.fillAmount = player.playerStat.data.expCount / player.playerStat.data.exp;
        }
        if (expText != null)
        {
            expText.text = Mathf.FloorToInt(player.playerStat.data.expCount).ToString() + "/" + Mathf.FloorToInt(player.playerStat.data.exp).ToString();
        }

        // 스킬 UI 업데이트 (Space 스킬)
        if (player.playerStat.spaceSkill != null)
        {
            if (spaceSkillImage != null)
            {
                // 스킬 레벨이 0이면 회색으로, 아니면 흰색으로 표시
                spaceSkillImage.color = player.playerStat.spaceSkill.IsLevel0() ? Color.gray : Color.white;
                // 스킬 쿨타임 진행도 표시
                spaceSkillImage.fillAmount = player.playerStat.spaceSkill.timer / player.playerStat.spaceSkill.cooltime;
            }
            if (spaceSkillText != null)
            {
                // 남은 쿨타임 계산
                float remainingCooltime = player.playerStat.spaceSkill.cooltime - player.playerStat.spaceSkill.timer;
                // 쿨타임이 0이면 공백, 아니면 남은 시간 표시
                spaceSkillText.text = (remainingCooltime <= 0) ? " " : Mathf.FloorToInt(remainingCooltime).ToString();
            }
        }

        // 스킬 UI 업데이트 (C 스킬)
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

        // 스킬 UI 업데이트 (R 스킬)
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