using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    public TMP_Text questNameText;
    public TMP_Text questDescText;
    public TMP_Text requirementText;


    public void Setup(QuestData data, QuestProgress progress)
    {
        questNameText.text = data.questName;
        questDescText.text = data.questDesc;

        string req = "";

        if (data.targetEnemyCount > 0)
        {
            int cur = progress != null ? progress.currentEnemyCount : data.targetEnemyCount;
            int max = data.targetEnemyCount;
            req += $"{data.targetEnemyName}: {cur} / {max}";
        }

        if (data.targetItem != null && data.targetItemCount > 0)
        {
            int cur = progress != null ? progress.currentItemCount : data.targetItemCount;
            int max = data.targetItemCount;
            req += $"{data.targetItem.itemName}: {cur} / {max}";
        }

        requirementText.text = req;
    }
}
