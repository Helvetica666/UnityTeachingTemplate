using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ChoiceUI：选择题界面（问题文字 + A / B / C 三个按钮）
public class ChoiceUI : MonoBehaviour
{
    [Header("面板")]
    public GameObject choicePanel;   // 整个选择题面板

    [Header("文字")]
    public TMP_Text questionText;    // 问题
    public TMP_Text choiceAText;     // 选项 A 按钮文字
    public TMP_Text choiceBText;     // 选项 B 按钮文字
    public TMP_Text choiceCText;     // 选项 C 按钮文字

    [Header("按钮")]
    public Button choiceAButton;
    public Button choiceBButton;
    public Button choiceCButton;

    private ChoiceEvent currentEvent; // 当前正在显示的选择题

    private void Awake()
    {
        choicePanel.SetActive(false); // 开始时隐藏
    }

    // 显示选择题（由 ChoiceEvent.Show 调用）
    public void ShowChoice(ChoiceEvent choiceEvent)
    {
        if (choiceEvent == null) return;

        currentEvent = choiceEvent;
        questionText.text = choiceEvent.question;

        choiceAText.text = choiceEvent.choiceA != null ? choiceEvent.choiceA.choiceText : "A";
        choiceBText.text = choiceEvent.choiceB != null ? choiceEvent.choiceB.choiceText : "B";

        // 选项 C 不存在时自动隐藏第三个按钮
        bool hasC = choiceEvent.choiceC != null && !string.IsNullOrEmpty(choiceEvent.choiceC.choiceText);
        choiceCButton.gameObject.SetActive(hasC);
        if (hasC)
        {
            choiceCText.text = choiceEvent.choiceC.choiceText;
        }

        choicePanel.SetActive(true);
    }

    // 按钮点击事件（Inspector 里绑定）
    public void OnChoiceA()
    {
        Choose(currentEvent != null ? currentEvent.choiceA : null);
    }

    public void OnChoiceB()
    {
        Choose(currentEvent != null ? currentEvent.choiceB : null);
    }

    public void OnChoiceC()
    {
        Choose(currentEvent != null ? currentEvent.choiceC : null);
    }

    // 应用效果并关闭面板
    private void Choose(ChoiceOption option)
    {
        if (currentEvent != null)
        {
            currentEvent.ApplyChoice(option);
        }

        currentEvent = null;
        choicePanel.SetActive(false);
    }
}
