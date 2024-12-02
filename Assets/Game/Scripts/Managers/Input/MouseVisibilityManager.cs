using UnityEngine;

public class MouseVisibilityManager : MonoBehaviour
{
    private bool _isMouseShown;

    public bool IsMouseShown
    {
        get => _isMouseShown;
        set => _isMouseShown = value;
    }

    public void ManageMouseVisibility()
    {
        Cursor.visible = IsMouseShown;
        Cursor.lockState = IsMouseShown ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    private void ManageMouseVisibility(bool showMouse)
    {
        IsMouseShown = showMouse;
        ManageMouseVisibility();
    }

    public void ShowMouse()
    {
        ManageMouseVisibility(true);
    }

    public void HideMouse()
    {
        ManageMouseVisibility(false);
    }
}
