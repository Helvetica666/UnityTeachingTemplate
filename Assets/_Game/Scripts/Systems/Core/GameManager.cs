using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private TMP_Text feedbackText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (feedbackPanel != null)
            feedbackPanel.SetActive(false);
    }

    public void ShowFeedback(string message, float duration = 3f)
    {
        CancelInvoke(nameof(HideFeedback));
        feedbackPanel.SetActive(true);
        feedbackText.text = message;
        Invoke(nameof(HideFeedback), duration);
    }

    private void HideFeedback()
    {
        feedbackPanel.SetActive(false);
    }
}
