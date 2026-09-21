using UnityEngine;
using UnityEngine.Events;

// ConditionalTrigger：变量满足条件时触发事件（例如 Value_A >= 70 时让 Door 出现）
public class ConditionalTrigger : MonoBehaviour
{
    public enum ConditionType
    {
        GreaterThan,      // 大于 >
        GreaterOrEqual,   // 大于等于 >=
        LessThan,         // 小于 <
        LessOrEqual,      // 小于等于 <=
        Equal             // 等于 ==
    }

    [Header("Condition")]
    public VariableDataSO targetVariable;                                    // 要检查的变量
    public ConditionType condition = ConditionType.GreaterOrEqual;           // 比较方式
    public float threshold = 70f;                                            // 阈值

    [SerializeField]
    public bool checkEveryFrame = true;

    [Header("Events")]
    public UnityEvent onConditionMet;    // 条件从不满足变为满足时触发
    public UnityEvent onConditionNotMet; // 条件从满足变为不满足时触发

    [Header("Combo Mode")]
    [Tooltip("与 AreaTrigger 组合使用（进入区域且条件满足才触发）。勾选后自动关闭每帧自检，并关闭同一物体上 AreaTrigger 的 Lock On Enter")]
    public bool useAreaConditional = false;

    private bool m_wasMet;

    private void Awake()
    {
        // 组合模式：自动关闭每帧自检，并让同一物体上的 AreaTrigger 不自动锁定
        if (useAreaConditional)
        {
            checkEveryFrame = false;

            AreaTrigger area = GetComponent<AreaTrigger>();
            if (area != null)
            {
                area.lockOnEnter = false;
                area.onEnter.AddListener(EvaluateNow);
            }
            else
            {
                Debug.LogWarning("ConditionalTrigger 已勾选 Use Area Conditional，但同一物体上没有 AreaTrigger 组件。请把 AreaTrigger 也挂到本物体上。", this);
            }
        }

        // 记住初始状态，避免开场立刻触发事件
        m_wasMet = IsMet();
    }

    private void Update()
    {
        if (checkEveryFrame)
        {
            CheckAndTrigger();
        }
    }

    // 检查一次条件，状态变化时触发对应事件（也可由其他组件调用）
    public void CheckAndTrigger()
    {
        bool met = IsMet();

        if (met && !m_wasMet)
        {
            m_wasMet = true;
            onConditionMet?.Invoke();
        }
        else if (!met && m_wasMet)
        {
            m_wasMet = false;
            onConditionNotMet?.Invoke();
        }
    }

    // 立即检查一次条件并触发对应事件（忽略状态变化记忆）
    // 用于"进入区域且条件满足"这类组合：由 AreaTrigger.onEnter 调用
    public void EvaluateNow()
    {
        if (IsMet())
        {
            onConditionMet?.Invoke();
        }
        else
        {
            onConditionNotMet?.Invoke();
        }
    }

    private bool IsMet()
    {
        if (targetVariable == null) return false;

        float value = targetVariable.CurrentValue;

        switch (condition)
        {
            case ConditionType.GreaterThan:    return value > threshold;
            case ConditionType.GreaterOrEqual: return value >= threshold;
            case ConditionType.LessThan:       return value < threshold;
            case ConditionType.LessOrEqual:    return value <= threshold;
            case ConditionType.Equal:          return Mathf.Approximately(value, threshold);
        }

        return false;
    }
}
