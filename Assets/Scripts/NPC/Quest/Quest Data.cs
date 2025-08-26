using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest/New Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("Quest info")]
    public int questID;
    public string questName;
    [TextArea] public string questDesc;

    [Header("Target enemy")]
    public int targetEnemyCode;
    public int targetEnemyCount;
    public string targetEnemyName;

    [Header("Target item")]
    public Item targetItem;
    public int targetItemCount;

    [Header("Reward")]
    public int rewardCoin;
    public Item rewardItem;
    public int rewardItemCount;
}
