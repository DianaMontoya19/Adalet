using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _UIContainer;

    public virtual void Activate()
    {
        _UIContainer.SetActive(true);
    }

    public virtual void Deactivate()
    {
        _UIContainer.SetActive(false);
    }
}
