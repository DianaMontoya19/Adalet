using UnityEngine;

public class HackWall : MonoBehaviour
{
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(LayerStrings.HackWall);
    }
}
