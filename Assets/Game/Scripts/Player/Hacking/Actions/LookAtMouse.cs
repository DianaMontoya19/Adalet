using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector2 _mousePos;
    private Vector3 _objectPos;
    private float _angle;

    private InputAction _mousePosAction;

    private void Awake()
    {
        _mousePosAction = InputSystem.actions.FindAction(InputStrings.Look);
    }

    private void OnEnable()
    {
        _mousePosAction.performed += LookAtTarget;
        _mousePosAction.canceled += LookAtTarget;
    }

    private void OnDisable()
    {
        _mousePosAction.performed -= LookAtTarget;
        _mousePosAction.canceled -= LookAtTarget;
    }

    private void LookAtTarget(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();

        _objectPos = Camera.main.WorldToScreenPoint(_target.position);
        _mousePos.x -= _objectPos.x;
        _mousePos.y -= _objectPos.y;

        _angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
        _target.rotation = Quaternion.Euler(new Vector3(0, -_angle + 90, 0));
    }
}
