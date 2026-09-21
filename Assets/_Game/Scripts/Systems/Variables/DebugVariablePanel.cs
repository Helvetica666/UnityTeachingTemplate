using UnityEngine;
using TMPro;

// DebugVariablePanel：教学 / Playtest 用，实时显示所有变量的值
public class DebugVariablePanel : MonoBehaviour
{
    [Header("显示开关")]
    public bool showDebugVariables = true; // 勾选显示，取消勾选则整体关闭

    [Header("变量")]
    public VariableDataSO[] variables;     // 要显示的变量

    [Header("UI 文字")]
    public TMP_Text[] valueTexts;          // 每行一个 Text（顺序与 variables 一致）

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        // 整体关闭：取消勾选后隐藏整个面板
        if (!showDebugVariables)
        {
            gameObject.SetActive(false);
            return;
        }

        // 每帧刷新：任何系统改完变量后，UI 立即更新
        Refresh();
    }

    // 刷新所有数值文字
    public void Refresh()
    {
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i] != null && valueTexts[i] != null)
            {
                valueTexts[i].text = $"{variables[i].displayName}: {variables[i].CurrentValue}";
            }
        }
    }
}
