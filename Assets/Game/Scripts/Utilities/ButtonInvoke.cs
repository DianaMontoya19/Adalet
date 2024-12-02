using UnityEngine;

public class ButtonInvoke : PropertyAttribute
{
    /// <summary>
    /// If you choose to display in editor or both, make sure that your script has a flag to run in editor.
    /// </summary>
    public enum DisplayIn
    {
        PlayMode,
        EditMode,
        PlayAndEditModes
    }

    public readonly string customLabel;
    public readonly string methodName;
    public readonly object methodParameters;
    public readonly DisplayIn displayIn;

    /// <summary>
    /// Add this attribute to any dummy field in order to show a button in inspector.
    /// </summary>
    /// <param name="methodName">Name of the method to call.</param>
    /// <param name="methodParameters">Optional parameters to pass into the method.</param>
    /// <param name="displayIn">Should the button show in play mode, edit mode, or both.</param>
    /// <param name="customLabel">Optional custom label.</param>
    public ButtonInvoke(
        string methodName,
        object methodParameters = null,
        DisplayIn displayIn = DisplayIn.PlayMode,
        string customLabel = ""
    )
    {
        this.methodName = methodName;
        this.methodParameters = methodParameters;
        this.displayIn = displayIn;
        this.customLabel = customLabel;
    }
}
