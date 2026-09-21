using UnityEngine;

// LabSelector：点击顶部 Lab 按钮，只显示对应的 Lab，隐藏其他 Lab
// 教学概念：按钮 -> 点击事件 -> SetActive 显示 / 隐藏
public class LabSelector : MonoBehaviour
{
    // 5 个 Lab 场景区域（顺序：Movement / Trigger / Variable / Condition / Component）
    public GameObject[] m_labAreas;

    // 5 个 Lab 对应的 UI 面板（顺序同上；没有 UI 的面板留空即可）
    public GameObject[] m_labUIs;

    // 按钮点击后调用：ShowLab(0) = Movement Lab，ShowLab(4) = Component Lab
    public void ShowLab(int index)
    {
        for (int i = 0; i < m_labAreas.Length; i++)
        {
            bool isShow = i == index;

            if (m_labAreas[i] != null)
            {
                m_labAreas[i].SetActive(isShow);
            }

            if (m_labUIs != null && i < m_labUIs.Length && m_labUIs[i] != null)
            {
                m_labUIs[i].SetActive(isShow);
            }
        }
    }
}
