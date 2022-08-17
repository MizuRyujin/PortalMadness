using UnityEngine;

public class Frisbee : PickableObject
{
    protected override void UseItem(PlayerHand hand)
    {
        Debug.Log("Was used!");
        rb.isKinematic = false;
        spriteCollider.enabled = true;
        rb.AddForce((hand.transform.up * hand.ThrowStrength * 0.25f) + hand.transform.right * hand.ThrowStrength);
        pickedUp = false;
    }
}
