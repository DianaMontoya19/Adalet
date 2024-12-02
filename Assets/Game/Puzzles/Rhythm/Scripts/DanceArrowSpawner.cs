using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DanceArrowSpawner : MonoBehaviour
{
    [Header("Heights")]
    [Range(1.0f, 10.0f)]
    [SerializeField]
    private int _minHeightY;

    [Range(1.0f, 10.0f)]
    [SerializeField]
    private int _maxHeightY;

    [Header("Dance Arrow")]
    [SerializeField]
    private DanceArrow _danceArrowPrefab;

    [SerializeField]
    private Transform _danceArrowHolder;

    [SerializeField]
    private List<Transform> _arrowSpawns;
    public ObjectPool<DanceArrow> DanceArrowPool;

    private void Awake()
    {
        DanceArrowPool = new ObjectPool<DanceArrow>(
            CreateArrow,
            OnTakeArrowFromPool,
            OnReturnArrowFromPool,
            OnDestroyArrow,
            true,
            10,
            25
        );
    }

    private DanceArrow CreateArrow()
    {
        int randomIndex = Random.Range(0, 4);

        Quaternion selectedRotation = Quaternion.identity;
        switch (randomIndex)
        {
            case 0:
                selectedRotation = Quaternion.Euler(0, 0, -90);
                break;
            case 1:
                selectedRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2:
                selectedRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                selectedRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
        Vector3 selectedSpawnPoint = _arrowSpawns[randomIndex].position;

        float randomY = Random.Range(_minHeightY, _maxHeightY);
        Vector3 spawnPosition = new(selectedSpawnPoint.x, randomY, selectedSpawnPoint.z);

        // Create dance arrow
        DanceArrow arrow = Instantiate(
            _danceArrowPrefab,
            spawnPosition,
            selectedRotation,
            _danceArrowHolder
        );

        // Assign projectile pool
        arrow.SetPool(DanceArrowPool);

        return arrow;
    }

    private void OnTakeArrowFromPool(DanceArrow danceArrow)
    {
        danceArrow.gameObject.SetActive(true);
    }

    private void OnReturnArrowFromPool(DanceArrow danceArrow)
    {
        danceArrow.gameObject.SetActive(false);
    }

    private void OnDestroyArrow(DanceArrow danceArrow)
    {
        Destroy(danceArrow.gameObject);
    }
}
