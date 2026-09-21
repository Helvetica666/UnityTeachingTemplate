using UnityEngine;
using UnityEngine.Events;

public class InteractionDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private LayerMask interactableLayer = ~0;

    [Header("Prompt Settings")]
    [SerializeField] private bool showInteractionPrompt = true;

    [Header("Events")]
    public UnityEvent<IInteractable> onInteractableFound;
    public UnityEvent<IInteractable> onInteractableLost;
    public UnityEvent<IInteractable> onInteraction;

    private IInteractable currentInteractable;

    private void Update()
    {
        IInteractable nearest = FindNearestInteractable();

        if (nearest != currentInteractable)
        {
            if (currentInteractable != null)
                onInteractableLost?.Invoke(currentInteractable);

            currentInteractable = nearest;

            if (currentInteractable != null)
                onInteractableFound?.Invoke(currentInteractable);
        }

        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
            onInteraction?.Invoke(currentInteractable);
        }
    }

    private IInteractable FindNearestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        IInteractable nearest = null;
        float minDist = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable == null) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = interactable;
            }
        }

        return nearest;
    }

    public IInteractable CurrentInteractable => currentInteractable;
    public bool ShowPrompt => showInteractionPrompt;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0.5f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
