using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();
                if (instance == null)
                {
                    GameObject gameObj =
                        new(typeof(T).Name) { hideFlags = HideFlags.HideAndDontSave };
                    instance = gameObj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public static bool HasInstance => instance != null;

    public float InitializationTimer { get; private set; }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        InitializationTimer = Time.time;
        DontDestroyOnLoad(gameObject);

        T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach (T oldInstance in oldInstances)
        {
            if (oldInstance.GetComponent<Singleton<T>>().InitializationTimer < InitializationTimer)
            {
                Destroy(gameObject);
            }
        }

        if (instance == null)
        {
            instance = this as T;
        }
    }
}
