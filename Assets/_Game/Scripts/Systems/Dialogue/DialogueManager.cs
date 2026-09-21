using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Reference")]
    [SerializeField] private DialogueUI dialogueUI;

    [Header("Events")]
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    private DialogueData currentData;
    private int currentLineIndex;
    private bool isDialogueActive;

    // 当前对话结束后的单次回调（由发起对话的 NPC 设置，例如弹选择题）
    private Action m_endAction;

    public bool IsDialogueActive => isDialogueActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 设置当前对话结束时要执行的逻辑；传 null 则对话结束不做额外处理
    public void SetEndAction(Action action)
    {
        m_endAction = action;
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines.Length == 0) return;

        currentData = data;
        currentLineIndex = 0;
        isDialogueActive = true;

        dialogueUI.Show(data.speakerName, data.lines[0]);
        onDialogueStart?.Invoke();
    }

    public void ContinueDialogue()
    {
        if (!isDialogueActive) return;

        currentLineIndex++;

        if (currentLineIndex >= currentData.lines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueUI.UpdateLine(currentData.lines[currentLineIndex]);
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        currentData = null;
        dialogueUI.Hide();
        onDialogueEnd?.Invoke();

        // 触发发起者设置的回调（如 NPC 的结束选择题），用完即清空
        Action action = m_endAction;
        m_endAction = null;
        action?.Invoke();
    }
}
