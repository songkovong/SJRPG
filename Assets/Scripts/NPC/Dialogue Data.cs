using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC Dialogue/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string dialougeID;
    [TextArea(2, 5)]
    public List<string> lines;
    [TextArea(2, 5)]
    public List<string> questProgressLines;
    [TextArea(2, 5)]
    public List<string> afterQuestLines;

    public bool tirggerQuest;
    public int questID;
    public bool openShop;
    public string shopID;

    public List<string> GetDialogueLines()
    {
        if (tirggerQuest)
        {
            if (QuestManager.Instance != null && QuestManager.Instance.IsQuestComplete(questID))
            {
                return afterQuestLines;
            }

            if (QuestManager.Instance != null && QuestManager.Instance.HasQuest(questID))
            {
                return questProgressLines;
            }

            return lines;
        }

        return lines;
    }
}
