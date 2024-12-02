using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryUI;

    private bool _isInventoryOpen = false;
    private bool _canOpenInventory = true;

    private List<InventorySlot> _inventoryContent = new();

    private InputAction _menuAction;

    private void OnEnable()
    {
        _menuAction.performed += ManageInventoryVisibilty;
    }

    private void OnDisable()
    {
        _menuAction.performed -= ManageInventoryVisibilty;
    }

    public bool AddToInventory(GameObject item, int quantity = 1)
    {
        return false;
    }

    public bool RemoveFromInventory(GameObject item, int quantity = 1, bool removeCompletly = false)
    {
        return false;
    }

    private void FindEmptySlot() { }

    #region Visibility
    private void ManageInventoryVisibilty(InputAction.CallbackContext context)
    {
        if (_canOpenInventory)
        {
            // Manages opening or closing the inventory based on its current visibility
            _isInventoryOpen = !_isInventoryOpen;

            if (_isInventoryOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        //* Eventually add extra flair and logic
        _inventoryUI.SetActive(true);
    }

    private void CloseInventory()
    {
        //* Eventually add extra flair and logic
        _inventoryUI.SetActive(false);
    }
}
    #endregion

public class InventorySlot
{
    public ItemData Item;
    public int Quantity;
}
