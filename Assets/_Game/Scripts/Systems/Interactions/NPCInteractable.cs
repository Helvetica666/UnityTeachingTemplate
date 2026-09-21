using UnityEngine;
using UnityEngine.Events;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [Header("NPC Info")]
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private string interactionPrompt = "Talk";

    [Header("Dialogue")]
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private bool canRepeatDialogue = true;

    [Header("End Choice")]
    [Tooltip("对话结束后弹出的选择题；留空则不弹任何选择题")]
    [SerializeField] private ChoiceEvent endChoice;

    [Header("Events")]
    public UnityEvent onInteract;

    private bool hasTalked;

    public string InteractionPrompt => interactionPrompt;
    public string InteractionText => dialogueData != null ? dialogueData.lines[0] : "...";

    public void Interact()
    {
        if (!canRepeatDialogue && hasTalked) return;

        hasTalked = true;
        onInteract?.Invoke();

        if (dialogueData != null)
        {
            // 对话结束后弹自己的选择题（endChoice 留空 = 不弹）
            DialogueManager.Instance.SetEndAction(endChoice != null ? () => endChoice.Show() : null);
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }
}
