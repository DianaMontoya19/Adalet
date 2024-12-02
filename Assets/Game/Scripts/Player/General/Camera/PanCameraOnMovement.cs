using PrimeTween;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineRecomposer))]
public class PanCameraOnMovement : MonoBehaviour
{
    private CinemachineRecomposer _recomposer;

    [SerializeField]
    private float _panAmount;

    [SerializeField]
    private float _panDuration;

    [SerializeField]
    private Ease _ease = Ease.Default;
    private InputAction _moveAction;
    private Vector3 _moveInput;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction(InputStrings.Move);
    }

    private void OnEnable()
    {
        _moveAction.performed += SetMove;
    }

    private void Start()
    {
        _recomposer = GetComponent<CinemachineRecomposer>();
    }

    private void OnDisable()
    {
        _moveAction.performed -= SetMove;
    }

    private void Update()
    {
        PanCamera();
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void PanCamera()
    {
        float targetPan = _moveInput.x * _panAmount;

        if (_recomposer.Pan != targetPan)
        {
            Tween.Custom(
                startValue: _recomposer.Pan,
                endValue: targetPan,
                duration: _panDuration,
                onValueChange: v => _recomposer.Pan = v,
                _ease
            );
        }
    }
}
