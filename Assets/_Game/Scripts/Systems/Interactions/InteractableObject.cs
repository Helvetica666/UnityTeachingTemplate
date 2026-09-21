using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Object Info")]
    [SerializeField] private string objectName = "Object";
    [SerializeField] private string interactionPrompt = "Examine";

    [TextArea(2, 5)]
    [SerializeField] private string interactionText = "Nothing happens.";

    [Header("Interaction Settings")]
    [SerializeField] private bool interactOnce = false;
    [SerializeField] private bool disableAfterInteraction = false;

    [Header("Events")]
    public UnityEvent onInteract;

    private bool hasInteracted;

    public string InteractionPrompt => interactionPrompt;
    public string InteractionText => interactionText;

    public void Interact()
    {
        if (interactOnce && hasInteracted) return;

        hasInteracted = true;
        onInteract?.Invoke();
        GameManager.Instance.ShowFeedback(interactionText);

        if (disableAfterInteraction)
            gameObject.SetActive(false);
    }
}
