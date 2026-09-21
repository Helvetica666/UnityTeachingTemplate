using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject dialoguePanel;

    [Header("Text")]
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Buttons")]
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject closeButton;

    [Header("打字机效果")]
    [Tooltip("每秒钟显示多少个字")]
    [SerializeField] private float typeSpeed = 20f;

    private string m_fullLine;
    private int m_charIndex;
    private float m_timer;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
    }

    public void Show(string speakerName, string line)
    {
        dialoguePanel.SetActive(true);
        speakerNameText.text = speakerName;
        StartTyping(line);
        continueButton.SetActive(true);
        closeButton.SetActive(true);
    }

    public void UpdateLine(string line)
    {
        StartTyping(line);
    }

    public void Hide()
    {
        dialoguePanel.SetActive(false);
    }

    // 开始打字：先清空文本，然后逐字显示
    private void StartTyping(string line)
    {
        m_fullLine = line;
        m_charIndex = 0;
        m_timer = 0f;
        dialogueText.text = string.Empty;

        // 速度小于等于 0 时直接显示完整文本，避免除零
        if (typeSpeed <= 0f)
        {
            dialogueText.text = m_fullLine;
            m_charIndex = m_fullLine != null ? m_fullLine.Length : 0;
        }
    }

    private void Update()
    {
        // 没有正在打字的文本就跳过
        if (m_fullLine == null || m_charIndex >= m_fullLine.Length) return;

        m_timer += Time.deltaTime;

        // 每攒够一个字的时间就多显示一个字
        while (m_timer >= 1f / typeSpeed)
        {
            m_timer -= 1f / typeSpeed;
            m_charIndex++;

            if (m_charIndex >= m_fullLine.Length)
            {
                // 全部显示完毕，直接显示完整文本避免漏字
                dialogueText.text = m_fullLine;
                return;
            }

            dialogueText.text = m_fullLine.Substring(0, m_charIndex);
        }
    }
}
