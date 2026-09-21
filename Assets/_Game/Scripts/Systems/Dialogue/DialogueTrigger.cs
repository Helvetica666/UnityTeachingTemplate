using UnityEngine;

// DialogueTrigger：通过 Inspector 配置并触发一段对话
public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;

    // 由 UnityEvent 调用：开始对话
    public void Play()
    {
        if (dialogueData != null && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }
}
