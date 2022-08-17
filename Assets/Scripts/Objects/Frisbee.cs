using UnityEngine;

public class Frisbee : PickableObject
{
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

    protected override void UseItem(PlayerHand hand)
    {
        Debug.Log("Was used!");
        _rb.isKinematic = false;
        _spriteCollider.enabled = true;
        _rb.AddForce(hand.transform.right * hand.ThrowStrength);
    }
}
