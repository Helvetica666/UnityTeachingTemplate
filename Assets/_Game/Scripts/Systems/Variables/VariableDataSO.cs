using UnityEngine;

// 通用游戏变量（Inspector 创建：右键 -> Create -> Game Data -> Variable）
[CreateAssetMenu(fileName = "VariableData", menuName = "Game Data/Variable")]
public class VariableDataSO : ScriptableObject
{
    [Header("信息")]
    public string variableName = "Value_A";      // 内部名称
    public string displayName = "Value A";       // UI 显示名称

    [Header("范围")]
    public float defaultValue = 50f;             // 每次运行开始时的值
    public float minValue = 0f;                  // 最小值（限制）
    public float maxValue = 100f;                // 最大值（限制）

    [Header("运行时")]
    [SerializeField] private float currentValue; // 当前值，由 VariableManager 管理

    public float CurrentValue => currentValue;   // 供其他脚本读取当前值

    // 重新运行时重置为默认值
    public void Initialize()
    {
        currentValue = Mathf.Clamp(defaultValue, minValue, maxValue);
    }

    // 加 / 减数值（自动限制在 Min / Max 之间）
    public void ChangeValue(float amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
    }

    // 直接设置数值（自动限制在 Min / Max 之间）
    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, minValue, maxValue);
    }
}
