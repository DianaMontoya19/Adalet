using UnityEngine;

public class FullscreenSwitch : MonoBehaviour
{
    [Header("Target Screen Sizes")]
    [SerializeField]
    private int _fullscreenPixelWidth = 1920;

    [SerializeField]
    private int _fullscreenPixelHeight = 1080;

    private bool _isFullscreen = false;

    private void Start()
    {
        _isFullscreen = Screen.fullScreen;
        SetFullScreenValues();
    }

    private void Update()
    {
        if (_isFullscreen != Screen.fullScreen)
        {
            if (Screen.fullScreen)
            {
                RestoreFullscreenResolution();
            }

            _isFullscreen = Screen.fullScreen;
        }
    }

    private void RestoreFullscreenResolution()
    {
        Screen.SetResolution(
            _fullscreenPixelWidth,
            _fullscreenPixelHeight,
            FullScreenMode.FullScreenWindow
        );
    }

    private void SetFullScreenValues()
    {
        // Set the screen width and height
        int systemWidth = Display.main.systemWidth;
        int systemHeight = Display.main.systemHeight;

        // Get a list of all supported resolutions
        Resolution[] supportedResolutions = Screen.resolutions;

        // Find the closest supported resolution to the native resolution
        Resolution closestResolution = supportedResolutions[0];
        int smallestGapInResolution = int.MaxValue;

        foreach (Resolution resolution in supportedResolutions)
        {
            int gap =
                Mathf.Abs(resolution.width - systemWidth)
                + Mathf.Abs(resolution.height - systemHeight);

            if (gap < smallestGapInResolution)
            {
                smallestGapInResolution = gap;
                closestResolution = resolution;
            }
        }

        _fullscreenPixelWidth = closestResolution.width;
        _fullscreenPixelHeight = closestResolution.height;
    }
}
