using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Player Data")]
    private Player _player;
    private Collider _playerCollider;
    private Vector3 _playerSpawnPosition;

    [Header("Spawn Point Data")]
    private Collider _spawnCollider;

    [SerializeField]
    private int _selectedSpawnID = -1;

    public void Start()
    {
        _player = MLocator.Instance.GameManager.ActivePlayer;
        if (_selectedSpawnID != -1)
        {
            _playerCollider = _player.GetComponentInChildren<Collider>();

            SetPlayerSpawnPoint();
        }
        else
        {
            _player?.gameObject.SetActive(false);
        }
    }

    public void SetPlayerSpawnPoint()
    {
        _player = MLocator.Instance.GameManager.ActivePlayer;
        if (_selectedSpawnID != -1)
        {
            _playerCollider = _player.GetComponentInChildren<Collider>();
        }
        else
        {
            _player?.gameObject.SetActive(false);
            return;
        }

        SpawnPoint[] spawnPoints = FindObjectsByType<SpawnPoint>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.ID == _selectedSpawnID)
            {
                _spawnCollider = spawnPoint.GetComponent<Collider>();
            }
        }

        _player.gameObject.SetActive(true);

        ResetPlayerVelocity();

        _player.transform.position = _spawnCollider.transform.position;
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;

        if (_spawnCollider)
        {
            _playerSpawnPosition =
                _spawnCollider.transform.position - new Vector3(0.0f, -colliderHeight, 0.0f);
        }
        /* else
        {
            _playerSpawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        } */
    }

    private void SnapToSurface()
    {
        Vector3 dir = new(0, -1, 0);
        if (_player.RB.SweepTest(dir, out RaycastHit hit))
        {
            gameObject.transform.position += dir * hit.distance;
        }
    }

    private void ResetPlayerVelocity()
    {
        Rigidbody playerRB = _player.GetComponent<Rigidbody>();
        playerRB.linearVelocity = Vector2.zero;
    }

    public void SetSpawnID(int spawnID = 0)
    {
        _selectedSpawnID = spawnID;
    }
}
