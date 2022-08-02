using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractables
{
    public void Interact() => OnInteract();
    protected abstract void OnInteract();
}
