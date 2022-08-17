using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float _handRange;
    [field: SerializeField] public float ThrowStrength { get ; private set ; }
    [SerializeField] private LayerMask _interactableLayer;

    private bool _isOccupied;
    private IInteractables _objInRange;
    private IInteractables _objInHand;

    public bool IsOccupied => _isOccupied;

    

    /// <summary>
    /// Interaction behaviour.
    /// </summary>
    /// <param name="interactor">GameObject that is interacting.</param>
    public void Interact(GameObject interactor)
    {
        if (_objInRange == null) return;

        _objInRange.OnInteract(interactor);
        if (_objInRange is PickableObject)
        {
            if (!_isOccupied)
            {
                AttachOrRemoveToHand(true, _objInRange as InteractableObject);
            }
            else
            {
                AttachOrRemoveToHand(false);
            }
        }
    }

    private void AttachOrRemoveToHand(bool attach, InteractableObject item = null)
    {
        _isOccupied = attach;
        if (attach)
        {
            _objInHand = item;
            item.transform.position = transform.position;
            item.transform.parent = transform;
        }
        else
        {
            InteractableObject obj = _objInHand as InteractableObject;
            obj.transform.parent = null;
            _objInHand = item;
        }
    }


    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        CheckForItems();
    }


    private void CheckForItems()
    {
        Collider2D[] hits = new Collider2D[1];
        int hitCount = Physics2D.OverlapBoxNonAlloc(transform.position, new Vector2(_handRange, _handRange * 1.12f), 0f, hits, _interactableLayer);
        if (hitCount > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] != null)
                {
                    if (hits[i].gameObject.TryGetComponent<InteractableObject>(out InteractableObject obj))
                    {
                        _objInRange = obj;
                        break;
                    }
                }
            }
        }
        else
        {
            _objInRange = null;
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_handRange, _handRange * 1.12f, 0.1f));
    }
}
