using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [Header("Area")]
    [SerializeField]
    private MusicArea _area;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(TagStrings.Player))
        {
            MLocator.Instance.AudioManager.SetMusicArea(_area);
        }
    }
}
