using UnityEngine;

public abstract class PickableObject : InteractableObject
{
    protected bool pickedUp;
    protected Rigidbody2D _rb;
    protected CircleCollider2D _spriteCollider;


    private new void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _spriteCollider = GetComponent<CircleCollider2D>();
    }
    protected override void Interacted(GameObject interactor)
    {
        if (interactor.TryGetComponent<PlayerHand>(out PlayerHand hand))
        {
            if (!hand.IsOccupied)
            {
                Debug.Log("Picked up!");
                _rb.isKinematic = true;
                _spriteCollider.enabled = false;
            }
            else
            {
                UseItem(hand);
            }
        }
    }

    protected abstract void UseItem(PlayerHand hand);
}
