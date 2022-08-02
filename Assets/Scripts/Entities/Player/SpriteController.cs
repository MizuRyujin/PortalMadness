using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private GameObject _runStopParticles;
    [SerializeField] private GameObject _jumpParticles;
    [SerializeField] private GameObject _fallParticles;
    private PlayerController _player;
    private GameObject _spriteObject;
    private Animator _animator;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();
        _spriteObject = _animator.gameObject;
    }

    /// <summary>>
    /// Update is called once per frame
    /// </summary>>
    private void Update()
    {
        FlipTowardsDirection();
        TriggerRunAnim();
    }

    private void FlipTowardsDirection()
    {
        if (_player.InputDir.x == 0f) return;

        Quaternion faceLeft = Quaternion.Euler(new Vector3(0f, 180f, 0f));

        if (_player.InputDir.x < 0.05f)
        {
            _spriteObject.transform.rotation = faceLeft;
        }
        else if (_player.InputDir.x > 0.05f)
        {
            _spriteObject.transform.rotation = Quaternion.identity;
        }
    }

    private void TriggerRunAnim()
    {
        _animator.SetBool("Grounded", _player.IsGrounded);
        if (_player.InputDir.x == 0f)
        {
            _animator.SetInteger("AnimState", 0);
            return;
        }
        _animator.SetInteger("AnimState", 1);
    }

    private void SpawnDustEffect(GameObject dustObject, float dustXOffset = 0f)
    {
        if (!dustObject)
        {
            Debug.LogWarning("No Particle System assigned. Verify object inspector");
            return;
        }

        Vector2 spawnPosition = transform.position + new Vector3(dustXOffset * _player.InputDir.x, 0.0f, 0.0f);

        dustObject.SetActive(true);
        dustObject.gameObject.transform.position = spawnPosition;

    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        // m_audioManager.PlaySound("RunStop");
        // Spawn Dust
        SpawnDustEffect(_runStopParticles, 0.6f);
    }

    void AE_footstep()
    {
        // m_audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        // m_audioManager.PlaySound("Jump");
        // Spawn Dust
        SpawnDustEffect(_jumpParticles);
    }

    void AE_Landing()
    {
        // m_audioManager.PlaySound("Landing");
        // Spawn Dust
        SpawnDustEffect(_fallParticles);
    }
}
