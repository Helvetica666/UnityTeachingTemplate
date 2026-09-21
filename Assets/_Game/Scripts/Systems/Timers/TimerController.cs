using UnityEngine;
using UnityEngine.Events;
using TMPro;

// TimerController：简单倒计时 / 正计时。到达时长后触发事件（可选显示 UI 文本）
public class TimerController : MonoBehaviour
{
    public enum TimerMode
    {
        CountDown, // 倒计时
        CountUp    // 正计时
    }

    [Header("Settings")]
    public float duration = 10f;
    public TimerMode mode = TimerMode.CountDown;
    public bool startAutomatically = true;

    [Header("UI (Optional)")]
    public TMP_Text timerText; // 可选：显示剩余 / 经过时间

    [Header("Events")]
    public UnityEvent onTimerFinished; // 到达时长时触发

    private bool m_isRunning;
    private float m_elapsed;

    public bool IsRunning => m_isRunning;

    private void Start()
    {
        if (startAutomatically)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        m_isRunning = true;
        m_elapsed = 0f;
    }

    private void Update()
    {
        if (!m_isRunning) return;

        m_elapsed += Time.deltaTime;

        // 可选：更新 UI 文本
        if (timerText != null)
        {
            float remaining = duration - m_elapsed;
            timerText.text = mode == TimerMode.CountDown
                ? $"{(remaining > 0f ? remaining : 0f):F1}"
                : $"{m_elapsed:F1}";
        }

        if (m_elapsed >= duration)
        {
            m_isRunning = false;
            onTimerFinished?.Invoke();
        }
    }
}
