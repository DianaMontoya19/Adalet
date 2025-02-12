using PrimeTween;
using UnityEngine;

public class ClueIndicator : MonoBehaviour
{
    [SerializeField]
    private float cycleDuration = 0.8f;

    [SerializeField]
    private float highIntensity = 1.2f;

    private Light clueLight;

    private void Awake()
    {
        clueLight = GetComponent<Light>();
    }

    private void Start()
    {
        Glint();
    }

    private void Glint()
    {
        Tween.LightIntensity(
            clueLight,
            highIntensity,
            cycleDuration,
            Ease.InOutSine,
            -1,
            CycleMode.Yoyo
        );
    }
}
