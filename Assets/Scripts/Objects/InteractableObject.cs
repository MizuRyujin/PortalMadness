using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractables
{
    public void OnInteract(GameObject interactor) => Interacted(interactor);
    protected abstract void Interacted(GameObject interactor);
}
