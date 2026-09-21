using UnityEngine;

// SimpleVariable：用一个数字表示游戏里的状态
// 教学概念：抽象状态可以用数字表示，UI 按钮可以直接改变它
public class SimpleVariable : MonoBehaviour
{
    // 当前数值，在 Inspector 里可以直接看到和修改
    public float value = 50f;

    // 加上 amount（按钮 +10 会调用）
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
