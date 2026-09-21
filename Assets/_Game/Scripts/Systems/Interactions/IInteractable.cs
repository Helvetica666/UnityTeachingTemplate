using UnityEngine;

public interface IInteractable
{
    string InteractionPrompt { get; }
    string InteractionText { get; }
    GameObject gameObject { get; }
    void Interact();
}
