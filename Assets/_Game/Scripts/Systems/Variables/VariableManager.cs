using UnityEngine;

// VariableManager：负责初始化变量、读取 / 修改数值、通知 Debug UI 更新
public class VariableManager : MonoBehaviour
{
    [Header("变量列表")]
    public VariableDataSO[] variables;           // 所有游戏变量

    [Header("Debug UI")]
    public DebugVariablePanel debugPanel;        // 可选的调试面板

    private void Awake()
    {
        // 每次运行都从默认值重新开始
        for (int i = 0; i < variables.Length; i++)
        {
            variables[i].Initialize();
        }
    }

    // 读取某个变量的当前值
    public float GetValue(VariableDataSO variable)
    {
        return variable != null ? variable.CurrentValue : 0f;
    }

    // 修改数值（加 / 减），并通知 Debug UI 立即更新
    public void ChangeValue(VariableDataSO variable, float amount)
    {
        if (variable == null) return;

        variable.ChangeValue(amount);
        RefreshDebugPanel();
    }

    // 直接设置数值，并通知 Debug UI 立即更新
    public void SetValue(VariableDataSO variable, float value)
    {
        if (variable == null) return;

        variable.SetValue(value);
        RefreshDebugPanel();
    }

    // 通知 Debug UI 立即更新
    private void RefreshDebugPanel()
    {
        if (debugPanel != null)
        {
            debugPanel.Refresh();
        }
    }
}
