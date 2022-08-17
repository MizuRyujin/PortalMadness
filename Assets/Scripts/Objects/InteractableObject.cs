using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractableObject : MonoBehaviour, IInteractables
{

    [SerializeField] protected Sprite sprite;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D interactionBox;

    public void OnInteract(GameObject interactor) => Interacted(interactor);

    /// <summary>
    /// Method that contains the behaviour of the object. Called when an interactor
    /// uses the interface's method OnInteract.
    /// </summary>
    /// <param name="interactor">The GameObject that is interacting.</param>
    protected abstract void Interacted(GameObject interactor);

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    protected void OnValidate()
    {
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        interactionBox = GetComponent<BoxCollider2D>();
        interactionBox.isTrigger = true;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    protected void Awake()
    {
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        interactionBox = GetComponent<BoxCollider2D>();
        interactionBox.isTrigger = true;
    }
}
