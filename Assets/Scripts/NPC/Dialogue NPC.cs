using UnityEngine;

public class DialogueNPC : NPCBase
{
    [SerializeField] private DialogueData dialogueData;

    public override void Enteract()
    {
        if (!DialogueManager.Instance.IsPlaying())
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }
}
