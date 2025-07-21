using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC Dialogue/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string dialougeID;
    [TextArea(2, 5)]
    public List<string> lines;

    public bool tirggerQuest;
    public string questID;
    public bool openShop;
    public string shopID;
}
