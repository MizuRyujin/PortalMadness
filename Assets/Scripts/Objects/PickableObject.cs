using UnityEngine;

public class PickableObject : InteractableObject
{
    [SerializeField] private Sprite _sprite;
    private SpriteRenderer _renderer;

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
    }
    
    protected override void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
