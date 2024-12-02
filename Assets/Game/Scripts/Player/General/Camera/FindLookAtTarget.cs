using Unity.Cinemachine;
using UnityEngine;

public class FindLookAtTarget : MonoBehaviour
{
    private CinemachineCamera _cinemachineCamera;

    private void Start()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();

        _cinemachineCamera.Target.TrackingTarget = MLocator
            .Instance
            .GameManager
            .ActivePlayer
            .transform;
    }
}
