using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float _handRange;
    private bool _isOccupied;
    private IInteractables _objInRange;
    private IInteractables _objInHand;

    public bool IsOccupied => _isOccupied;

    public void Interact(GameObject interactor)
    {
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

    public void AttachOrRemoveToHand(bool attach, InteractableObject item = null)
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
            _objInHand = null;
            item.transform.parent = null;
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
        Collider2D[] hits = new Collider2D[10];
        int hitCount = Physics2D.OverlapBoxNonAlloc(transform.position, new Vector2(_handRange, _handRange * 1.12f), 0f, hits);
        if (hitCount > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] != null)
                {
                    if (hits[i].gameObject.TryGetComponent<InteractableObject>(out InteractableObject obj))
                    {
                        Debug.Log("Interactable in range!");
                        _objInRange = obj;
                        break;
                    }
                }
            }
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
