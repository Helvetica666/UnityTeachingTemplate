using UnityEngine;

// SimpleCondition：条件 → 反馈
// 教学概念：if + >= + 条件 + 规则 + 反馈
public class SimpleCondition : MonoBehaviour
{
    // 当前数值
    public float value = 50f;

    // 达标线：修改这个值，观察 Door 的变化
    public float threshold = 70f;

    // 要显示 / 隐藏的物体（Door）
    public GameObject resultObject;

    private void Update()
    {
        // 每帧检查条件：达标就显示，没达标就隐藏
        if (value >= threshold)
        {
            resultObject.SetActive(true);
        }
        else
        {
            resultObject.SetActive(false);
        }
    }
     public void AddValue(float amount)
    {
        value += amount;
        Debug.Log($"当前 Value = {value}");
    }

    // 减去 amount（按钮 -10 会调用）
    public void SubtractValue(float amount)
    {
        value -= amount;
        Debug.Log($"当前 Value = {value}");
    }
}
