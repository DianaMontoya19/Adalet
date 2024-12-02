using UnityEngine;
using UnityEngine.InputSystem;

public class HackingMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Movement")]
    [SerializeField]
    private PlayerMovementData _data;
    private float _movementSpeed = 0;

    private InputAction _moveAction;

    private Vector3 _moveInput;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _moveAction = InputSystem.actions.FindAction(InputStrings.Move);
    }

    private void Start()
    {
        // Deactivate default gravity
        _rb.useGravity = false;

        // Freezes player rotation
        _rb.freezeRotation = true;

        // Apply linear damping
        _rb.linearDamping = _data.GroundDrag;
    }

    private void OnEnable()
    {
        _moveAction.performed += SetMove;
        _moveAction.canceled += SetMove;
    }

    private void OnDisable()
    {
        _moveAction.performed -= SetMove;
        _moveAction.canceled -= SetMove;
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region MOVEMENT
    private void SetMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y).normalized;
    }

    private void Move()
    {
        // Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.magnitude * _data.MaxSpeed;

        // Gets an acceleration value based on if we are accelerating or trying to decelerate.
        float accelRate;

        accelRate =
            (Mathf.Abs(targetSpeed) > 0.01f) ? _data.WalkAccelAmount : _data.WalkDecelAmount;

        // Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - _rb.linearVelocity.magnitude;

        // Calculate force along x-axis to apply to the player
        _movementSpeed = speedDif * accelRate;

        _rb.AddForce(_movementSpeed * _moveDirection, ForceMode.Force);
    }
    #endregion
}
