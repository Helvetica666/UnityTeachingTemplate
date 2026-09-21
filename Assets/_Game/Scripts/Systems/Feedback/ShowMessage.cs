using System.Collections;
using UnityEngine;
using TMPro;

// ShowMessage：显示一条消息，几秒后自动隐藏
public class ShowMessage : MonoBehaviour
{
    [Header("Message")]
    public TMP_Text targetText;      // 显示文字的 Text（可空）
    public GameObject messagePanel;  // 要显示 / 隐藏的面板（可空）
    public string message = "Hello!";
    public float displayDuration = 2f;

    private Coroutine m_hideCoroutine;

    // 由 UnityEvent 调用：显示消息
    public void Show()
    {
        if (targetText != null) targetText.text = message;
        if (messagePanel != null) messagePanel.SetActive(true);

        if (m_hideCoroutine != null) StopCoroutine(m_hideCoroutine);
        m_hideCoroutine = StartCoroutine(HideAfterCo());
    }

    // 立即隐藏（也可由 UnityEvent 调用）
    public void Hide()
    {
        if (m_hideCoroutine != null) StopCoroutine(m_hideCoroutine);
        if (messagePanel != null) messagePanel.SetActive(false);
    }

    private IEnumerator HideAfterCo()
    {
        yield return new WaitForSeconds(displayDuration);
        if (messagePanel != null) messagePanel.SetActive(false);
    }
}
