using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Expandable] private EntityStats _baseStats;
    [SerializeField] private float _jumpTimer;

    private PlayerInputs _playerActions;
    private Rigidbody2D _rb;
    private Vector2 _inputDir;
    private Vector2 _moveDir;
    private float _jumpTime;
    private float _baseGravity;
    private PlayerHand _hands;

    public Vector2 InputDir => _inputDir;
    public bool IsGrounded
    {
        get
        {
            Collider2D[] hits = new Collider2D[1];
            Physics2D.OverlapCircleNonAlloc(transform.position, 0.05f, hits, LayerMask.GetMask("Ground"));
            return hits[0] != null;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputDir = Vector2.zero;
        _moveDir = Vector2.zero;
        _jumpTime = _jumpTimer;
        _baseGravity = _rb.gravityScale;
        _hands = GetComponentInChildren<PlayerHand>();

        SetupActionMap();
        _playerActions.InGame.Jump.performed += ctx => Jump();
        _playerActions.InGame.Interact.performed += ctx => _hands.Interact(_hands.gameObject);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _inputDir = _playerActions.InGame.Move.ReadValue<Vector2>();
        ReduceGravity();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Define a slowdown velocity for when input in 0
        float slowDownDrag = _baseStats.Acceleration * 1.25f;

        if (_inputDir.x != 0)
        {
            _moveDir.x += _inputDir.x * _baseStats.Acceleration * Time.fixedDeltaTime;

            if (_moveDir.x > _baseStats.MaxSpeed)
                _moveDir.x = _baseStats.MaxSpeed;

            if (_moveDir.x < -_baseStats.MaxSpeed)
                _moveDir.x = -_baseStats.MaxSpeed;
        }
        else
        {
            // Apply slowdown when input is released
            if (_moveDir.x > 0.05f)
            {
                _moveDir.x -= slowDownDrag * Time.fixedDeltaTime;
            }
            else if (_moveDir.x < -0.05f)
            {
                _moveDir.x += slowDownDrag * Time.fixedDeltaTime;
            }
            else
            {
                _moveDir.x = 0f;
            }
        }

        _moveDir.y = _rb.velocity.y;
        _rb.velocity = _moveDir;
    }

    private void ReduceGravity()
    {
        bool jumpPressed = _playerActions.InGame.Jump.IsPressed();

        if (!jumpPressed || _jumpTime < 0f)
        {
            _rb.gravityScale = _baseGravity;

            if (!jumpPressed)
            {
                _jumpTime = _jumpTimer;
            }
        }
        else
        {
            _jumpTime -= Time.deltaTime;
            _rb.gravityScale = _baseGravity * 0.25f;
        }
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            _rb.AddForce(Vector2.up * _baseStats.JumpForce, ForceMode2D.Impulse);
        }
    }

    private void SetupActionMap()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerInputs();
            _playerActions.Enable();
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        SetupActionMap();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        _playerActions.Disable();
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
