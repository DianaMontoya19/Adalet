using UnityEngine;
using UnityEngine.UI;

public class CanvasScreenSizer : MonoBehaviour
{
    private CanvasScaler _canvasScale;

    private void Awake()
    {
        _canvasScale = GetComponent<CanvasScaler>();
        _canvasScale.referenceResolution = new(Screen.width, Screen.height);
    }
}
