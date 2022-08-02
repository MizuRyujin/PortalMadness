using System;
using UnityEngine;

public class PickableObject : InteractableObject
{
    [SerializeField] private Sprite _sprite;
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private BoxCollider2D _interactionBox;
    private CircleCollider2D _spriteCollider;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (_sprite != null)
        {
            _renderer.sprite = _sprite;
        }
        _interactionBox = GetComponent<BoxCollider2D>();
        _spriteCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
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
                UseItem();
            }
        }
    }

    private void UseItem()
    {
        Debug.Log("Was used!");
        _spriteCollider.enabled = true;
        _rb.isKinematic = false;
    }
}
