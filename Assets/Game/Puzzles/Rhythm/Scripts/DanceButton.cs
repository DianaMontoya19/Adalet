using UnityEngine;

public class DanceButton : MonoBehaviour
{
    public SpriteRenderer Renderer { get; private set; }

    public bool IsArrowInside { get; private set; } = false;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagStrings.DanceArrow))
        {
            IsArrowInside = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(TagStrings.DanceArrow))
        {
            IsArrowInside = false;
        }
    }
}
