using UnityEngine;

// 通用变量操作类型
public enum VariableOperation
{
    Add,      // 加
    Subtract, // 减
    Set       // 直接设置
}

// VariableModifier：通用变量修改组件，任何系统都可以调用 ApplyEffect()
public class VariableModifier : MonoBehaviour
{
    [Header("目标变量")]
    public VariableDataSO targetVariable;

    [Header("操作")]
    public VariableOperation operation = VariableOperation.Add;
    public float amount = 10f;

    // 由其他系统调用（Choice / Trigger / Interaction / Collectible）
    public void ApplyEffect()
    {
        if (targetVariable == null) return;

        switch (operation)
        {
            case VariableOperation.Add:
                targetVariable.ChangeValue(amount);
                break;

            case VariableOperation.Subtract:
                targetVariable.ChangeValue(-amount);
                break;

            case VariableOperation.Set:
                targetVariable.SetValue(amount);
                break;
        }
    }
}
