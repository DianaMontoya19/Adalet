using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    private InteractableObject _interactableObject;

    [field: SerializeField]
    public bool IsActive { get; set; } = true;

    private InputAction _interactAction;
    private bool _interactPressed;

    private void Awake()
    {
        _interactAction = InputSystem.actions.FindAction(InputStrings.Interact);
    }

    private void OnEnable()
    {
        if (IsActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }

        _interactAction.started += Interact;
        _interactAction.canceled += Interact;
    }

    private void OnDisable()
    {
        _interactAction.started -= Interact;
        _interactAction.canceled -= Interact;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagStrings.Player))
        {
            MLocator.Instance.InteractionUI.Activate();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(TagStrings.Player))
        {
            MLocator.Instance.InteractionUI.Deactivate();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag(TagStrings.Player) && _interactPressed)
        {
            _interactableObject.Interact();
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _interactPressed = context.started;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
