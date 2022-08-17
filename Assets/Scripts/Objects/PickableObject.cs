using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class PickableObject : InteractableObject
{
    /// <summary>
    /// Provides picked up state. Has to be returned to false by child classes
    /// </summary>
    protected bool pickedUp;
    /// <summary>
    /// The pickable object's Rigidbody2D
    /// </summary>
    protected Rigidbody2D rb;
    /// <summary>
    /// Collider to interact phisically with the world.
    /// </summary>
    protected CapsuleCollider2D spriteCollider;
    /// <summary>
    /// Reference to the PlayerHand script for use in methods
    /// </summary>
    protected PlayerHand hand;

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    private new void OnValidate()
    {
        base.OnValidate();
        spriteCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.drag = 1f;
    }

    private new void Awake()
    {
        base.Awake();
        spriteCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.drag = 1f;
    }
    protected override void Interacted(GameObject interactor)
    {
        if (pickedUp)
        {
            UseItem(this.hand);
            return;
        }
        if (interactor.TryGetComponent<PlayerHand>(out PlayerHand hand))
        {
            pickedUp = true;
            this.hand = hand;
            rb.isKinematic = true;
            spriteCollider.enabled = false;
        }
    }

    /// <summary>
    /// On second interaction, if item was picked up, this method is called
    /// </summary>
    /// <param name="hand">PlayerHand script reference</param>
    protected abstract void UseItem(PlayerHand hand);
}
