using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent;
    public UnityEvent onEventTriggered;

    private void OnEnable()
    {
        GameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        GameEvent.RemoveListener(this);
    }

    public void OnEventTriggered()
    {
        onEventTriggered?.Invoke();
    }
}
