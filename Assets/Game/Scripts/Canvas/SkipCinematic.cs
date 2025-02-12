using UnityEngine;

public class SkipCinematic : MonoBehaviour
{
    [SerializeField]
    private GameObject _cinematicCanvas;

    public bool IsCinematicComplete;

    private void Update()
    {
        SkipCurrentCinematic();
    }

    public void SkipCurrentCinematic()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsCinematicComplete = true;
            _cinematicCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void ChangeIsCinematicComplete()
    {
        IsCinematicComplete = true;
    }
}
