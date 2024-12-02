using UnityEngine;
using UnityEngine.Pool;

public class DanceArrow : MonoBehaviour
{
    [SerializeField]
    private float _beatTempo;

    private float _elapsedTime = 0f;

    [SerializeField]
    private float _increaseInterval;

    [SerializeField]
    private float _increaseAmount;

    private ObjectPool<DanceArrow> _danceArrowPool;

    private void Awake()
    {
        gameObject.tag = TagStrings.DanceArrow;
    }

    public void SetPool(ObjectPool<DanceArrow> pool)
    {
        _danceArrowPool = pool;
    }

    void Start()
    {
        _beatTempo /= 60f;
    }

    void Update()
    {
        transform.position -= new Vector3(0f, _beatTempo * Time.deltaTime, 0f);

        _elapsedTime += Time.deltaTime;

        // Aumenta el tempo despues de un cierto intervalo
        if (_elapsedTime >= _increaseInterval)
        {
            _beatTempo += _increaseAmount; // Aumenta el tempo
            _elapsedTime = 0f; // Reinicia el temporizador
        }
    }
}
