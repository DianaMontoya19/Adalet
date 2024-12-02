using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField]
    private string _parameterName;

    [SerializeField]
    private float _parameterValue;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(TagStrings.Player))
        {
            MLocator.Instance.AudioManager.SetAmbienceParameter(_parameterName, _parameterValue);
        }
    }
}
