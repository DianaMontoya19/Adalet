using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input Component")]
    [SerializeField]
    private PlayerInput _playerInput;

    [field: Header("Action Maps")]
    [field: SerializeField, ReadOnly]
    public string ActiveActionMap { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchActionMap(string actionMap = null)
    {
        if (actionMap.Equals(null))
        {
            _playerInput.actions.Disable();
        }

        _playerInput.SwitchCurrentActionMap(actionMap);

        Debug.Log($"Activated {_playerInput.currentActionMap.name} Action Map");

        ActiveActionMap = _playerInput.currentActionMap.name;
    }
}
