using UnityEngine;
using UnityEngine.Events;

// 单个选项的效果：修改一个变量
[System.Serializable]
public class ChoiceEffect
{
    public VariableDataSO targetVariable;  // 目标变量
    public VariableOperation operation;    // 操作：加 / 减 / 设置
    public float amount;                   // 数值
}

// 一个选项：文字 + 效果列表（可修改一个或多个变量）+ 选中时的事件（可触发结局等）
[System.Serializable]
public class ChoiceOption
{
    public string choiceText = "Choice";
    public ChoiceEffect[] effects;
    public UnityEvent onChosen; // 选中该选项后触发（如显示结局）
}

// ChoiceEvent：选择题事件（放在 PF_ChoiceEvent 预制体上）
public class ChoiceEvent : MonoBehaviour
{
    [Header("问题")]
    [TextArea(2, 4)]
    public string question = "What will you do?";

    [Header("选项")]
    public ChoiceOption choiceA;
    public ChoiceOption choiceB;
    public ChoiceOption choiceC; // 可空：留空则自动隐藏第三个按钮

    [Header("UI")]
    public ChoiceUI choiceUI;    // 指向场景里的 ChoiceUI

    // 由 NPC 对话结束等事件调用，弹出选择题
    public void Show()
    {
        if (choiceUI != null)
        {
            choiceUI.ShowChoice(this);
        }
    }

    // 应用一个选项的效果（由 ChoiceUI 在玩家选择后调用）
    public void ApplyChoice(ChoiceOption option)
    {
        if (option == null) return;

        if (option.effects != null)
        {
            for (int i = 0; i < option.effects.Length; i++)
            {
                ChoiceEffect effect = option.effects[i];
                if (effect == null || effect.targetVariable == null) continue;

                switch (effect.operation)
                {
                    case VariableOperation.Add:
                        effect.targetVariable.ChangeValue(effect.amount);
                        break;

                    case VariableOperation.Subtract:
                        effect.targetVariable.ChangeValue(-effect.amount);
                        break;

                    case VariableOperation.Set:
                        effect.targetVariable.SetValue(effect.amount);
                        break;
                }
            }
        }

        // 选项自身的事件（如触发结局）
        option.onChosen?.Invoke();
    }
}
