using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineConfiner3D))]
public class FindCameraBoundingVolume : MonoBehaviour
{
    private CinemachineConfiner3D _camConfiner;
    private GameObject _confinerObj = null;

    private void Start()
    {
        if (TryGetComponent<CinemachineConfiner3D>(out _camConfiner))
        {
            _confinerObj = GameObject.FindGameObjectWithTag(TagStrings.BoundingVolume);

            if (_confinerObj != null)
            {
                _camConfiner.BoundingVolume = _confinerObj.GetComponent<Collider>();
            }
        }
    }
}
