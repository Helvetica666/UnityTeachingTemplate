using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractionDetector detector;
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Start()
    {
        promptPanel.SetActive(false);

        detector.onInteractableFound.AddListener(ShowPrompt);
        detector.onInteractableLost.AddListener(HidePrompt);
    }

    private void ShowPrompt(IInteractable interactable)
    {
        if (!detector.ShowPrompt) return;
        promptPanel.SetActive(true);
        promptText.text = "Press E to " + interactable.InteractionPrompt;
    }

    private void HidePrompt(IInteractable interactable)
    {
        promptPanel.SetActive(false);
    }
}
