using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorationMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private Rigidbody _rb;
    private Collider _collider;

    [Header("Movement")]
    [SerializeField]
    private PlayerMovementData _data;

    private float _movementSpeed = 0;

    [Header("Slope Handling")]
    private RaycastHit _slopeHit;
    private bool _isExitingSlope;

    [Header("Rotation")]
    [SerializeField]
    private float _rotationSpeed;

    [Header("Audio")]
    [SerializeField]
    private LayerMask _floorLayer;

    [SerializeField]
    private float _footstepSpeed;
    private EventInstance _playerFootsteps;
    private float _footstepTimer = 0.0f;
    private Terrain _currentTerrain;

    private InputAction _moveAction;
    private InputAction _runAction;
    private Vector3 _moveInput;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _animator = FindFirstObjectByType<Animator>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<Collider>();

        _moveAction = InputSystem.actions.FindAction(InputStrings.Move);
        _runAction = InputSystem.actions.FindAction(InputStrings.Run);
    }

    private void OnEnable()
    {
        _moveAction.performed += SetMove;
        _moveAction.canceled += SetMove;
    }

    private void Start()
    {
        // Deactivate default gravity
        _rb.useGravity = false;

        // Freezes player rotation
        _rb.freezeRotation = true;

        // Apply linear damping
        _rb.linearDamping = _data.GroundDrag;

        // Create SFX instance for footsteps
        _playerFootsteps = MLocator
            .Instance
            .AudioManager
            .CreateInstance(MLocator.Instance.FMODEvents.PlayerFootsteps);

        _playerFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnDisable()
    {
        _moveAction.performed -= SetMove;
        _moveAction.canceled -= SetMove;
    }

    private void Update()
    {
        _footstepTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Apply custom gravity
        ApplyGravity();

        Move();

        Rotate();
    }

    #region MOVEMENT
    private void SetMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y).normalized;
        _animator.SetFloat("Velx", _moveInput.magnitude);
    }

    private void Move()
    {
        // Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.magnitude * _data.MaxSpeed;

        // Gets an acceleration value based on if we are accelerating or trying to decelerate.
        float accelRate;
        if (_runAction.IsPressed())
        {
            accelRate =
                (Mathf.Abs(targetSpeed) > 0.01f)
                    ? _data.WalkAccelAmount * _data.RunAccelMult
                    : _data.WalkDecelAmount * _data.RunDecelMult;
        }
        else
        {
            accelRate =
                (Mathf.Abs(targetSpeed) > 0.01f) ? _data.WalkAccelAmount : _data.WalkDecelAmount;
        }

        // Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - _rb.linearVelocity.magnitude;

        // Calculate force along x-axis to apply to the player
        _movementSpeed = speedDif * accelRate;

        if (IsOnSlope() && !_isExitingSlope)
        {
            Vector3 slopeMoveDirection = Vector3
                .ProjectOnPlane(_moveDirection, _slopeHit.normal)
                .normalized;

            _rb.AddForce(_movementSpeed * slopeMoveDirection, ForceMode.Force);
        }
        else
        {
            _rb.AddForce(_movementSpeed * _moveDirection, ForceMode.Force);
        }

        if (!_moveDirection.Equals(Vector3.zero))
        {
            ManageFootstepSFX();
        }
    }

    private bool IsOnSlope()
    {
        bool isOnSlope = Physics.Raycast(
            transform.position,
            Vector3.down,
            out _slopeHit,
            _collider.bounds.size.y * 0.5f + 0.3f
        );

        if (isOnSlope)
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _data.MaxSlopeAngle && angle != 0;
        }
        return false;
    }
    #endregion

    #region ROTATION
    private void Rotate()
    {
        if (_moveDirection != Vector3.zero)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(_moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                rotationTarget,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
    #endregion

    #region GRAVITY
    private void ApplyGravity()
    {
        if (!IsOnSlope())
        {
            Vector3 gravity = 9.81f * _data.GravityScale * Vector3.down;
            _rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
    #endregion

    #region SOUND EFFECTS
    private void ManageFootstepSFX()
    {
        if (_footstepTimer > _footstepSpeed)
        {
            DetermineTerrain();
            _playerFootsteps.setParameterByName("Terrain", (int)_currentTerrain);

            _playerFootsteps.getPlaybackState(out PLAYBACK_STATE playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                _playerFootsteps.start();
            }
            else
            {
                _playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }

            _footstepTimer = 0.0f;
        }
    }

    private void DetermineTerrain()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10.0f, _floorLayer);

        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.CompareTag(TagStrings.Concrete))
        {
            _currentTerrain = Terrain.CONCRETE;
        }
        else if (hitObj.CompareTag(TagStrings.Metal))
        {
            _currentTerrain = Terrain.METAL;
        }
    }

    public enum Terrain
    {
        CONCRETE,
        METAL,
    }
    #endregion
}
