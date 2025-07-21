using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    [SerializeField] protected string npcID;
    public abstract void Enteract();

    public bool isPlayerDetect { get; protected set; } = false;
    public GameObject EnteractionIcon;

    protected virtual void Start()
    {
        EnteractionIcon.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        isPlayerDetect = true;
        EnteractionIcon.SetActive(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        isPlayerDetect = false;
        EnteractionIcon.SetActive(false);

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsPlaying())
        {
            DialogueManager.Instance.EndDialogue();
        }
    }
}
