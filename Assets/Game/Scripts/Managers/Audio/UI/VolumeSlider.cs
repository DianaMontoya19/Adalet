using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        AMBIENCE,
        SFX
    }

    [Header("Type")]
    [SerializeField]
    private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = MLocator.Instance.AudioManager.MasterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = MLocator.Instance.AudioManager.MusicVolume;
                break;
            case VolumeType.AMBIENCE:
                volumeSlider.value = MLocator.Instance.AudioManager.AmbienceVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = MLocator.Instance.AudioManager.SFXVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                MLocator.Instance.AudioManager.MasterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                MLocator.Instance.AudioManager.MusicVolume = volumeSlider.value;
                break;
            case VolumeType.AMBIENCE:
                MLocator.Instance.AudioManager.AmbienceVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                MLocator.Instance.AudioManager.SFXVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }
}
