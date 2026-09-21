using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// EndingManager：显示结局面板，可选暂停玩家，并提供重开按钮
// 可由任意 UnityEvent 调用 ShowEnding(EndingDataSO) 来触发结局
public class EndingManager : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject endingPanel;   // 整个结局面板
    public TMP_Text titleText;       // 结局标题
    public TMP_Text descriptionText; // 结局描述
    public Image endingImage;        // 可选：结局图片（留空则不显示）

    [Header("Player")]
    [Tooltip("勾选后显示结局时禁用玩家（停止移动与交互）")]
    public bool pausePlayer = true;
    public GameObject playerObject;  // 玩家物体（暂停时禁用）

    private bool m_isEndingShown;

    public bool IsEndingShown => m_isEndingShown;

    // 显示结局（可由任意 UnityEvent 调用）
    public void ShowEnding(EndingDataSO data)
    {
        if (data == null || m_isEndingShown) return;

        m_isEndingShown = true;
        endingPanel.SetActive(true);
        titleText.text = data.endingTitle;
        descriptionText.text = data.endingDescription;

        // 可选图片：有图则显示，无图则隐藏
        if (endingImage != null)
        {
            endingImage.gameObject.SetActive(data.endingSprite != null);
            endingImage.sprite = data.endingSprite;
        }

        // 可选：暂停玩家移动与交互
        if (pausePlayer && playerObject != null)
        {
            playerObject.SetActive(false);
        }
    }

    // 重新加载当前场景（Restart 按钮调用）
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
