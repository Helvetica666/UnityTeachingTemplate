using UnityEngine;

// SpriteFade：挂在 SpriteRenderer 上，提供渐入 / 渐出方法供其他事件调用
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFade : MonoBehaviour
{
    [Header("Fade")]
    [Tooltip("渐入 / 渐出持续的时间（秒）")]
    public float fadeDuration = 1f;

    private SpriteRenderer m_spriteRenderer;
    private bool m_isFading;
    private bool m_fadeOut;      // true 表示正在渐出
    private float m_timer;
    private float m_startAlpha;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 渐入：先激活物体，再从当前透明度渐变到完全不透明
    public void FadeIn()
    {
        gameObject.SetActive(true);
        m_fadeOut = false;
        StartFade();
    }

    // 渐出：渐变到完全透明，结束后自动隐藏物体
    public void FadeOut()
    {
        m_fadeOut = true;
        StartFade();
    }

    private void StartFade()
    {
        m_isFading = true;
        m_timer = 0f;
        m_startAlpha = m_spriteRenderer.color.a;
    }

    private void Update()
    {
        if (!m_isFading) return;

        m_timer += Time.deltaTime;
        float progress = fadeDuration <= 0f ? 1f : Mathf.Clamp01(m_timer / fadeDuration);

        m_spriteRenderer.color = new Color(
            m_spriteRenderer.color.r,
            m_spriteRenderer.color.g,
            m_spriteRenderer.color.b,
            Mathf.Lerp(m_startAlpha, m_fadeOut ? 0f : 1f, progress)
        );

        if (progress >= 1f)
        {
            m_isFading = false;

            // 渐出结束后自动隐藏物体
            if (m_fadeOut)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
