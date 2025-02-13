using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField]
    private GameObject _loadingScreen;

    [SerializeField]
    private SceneField _mainMenuScene;

    [SerializeField]
    private float _fadeDurationSeconds = 0.35f;

    [SerializeField]
    private CanvasGroup _transitionCanvasGroup;

    public bool IsLoading { get; private set; }

    private void Start()
    {
        _loadingScreen.SetActive(false);
    }

    public void LoadScene(
        SceneField sceneToLoad,
        Action onFade = null,
        Action onComplete = null,
        LoadSceneMode mode = LoadSceneMode.Single
    )
    {
        if (!IsLoading)
        {
            IsLoading = true;
            StartCoroutine(LoadSceneCoroutine(sceneToLoad, onFade, onComplete, mode));
        }
    }

    public IEnumerator LoadSceneCoroutine(
        SceneField sceneToLoad,
        Action onFade = null,
        Action onComplete = null,
        LoadSceneMode mode = LoadSceneMode.Single
    )
    {
        _loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, _fadeDurationSeconds));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad, mode);

        onFade?.Invoke();

        while (!operation.isDone)
        {
            yield return null;
        }

        onComplete?.Invoke();
        yield return StartCoroutine(FadeLoadingScreen(0, _fadeDurationSeconds));
        _loadingScreen.SetActive(false);
        IsLoading = false;
    }

    public IEnumerator UnloadScene(
        SceneField sceneToUnload,
        Action onFade = null,
        Action onComplete = null
    )
    {
        _loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, _fadeDurationSeconds));

        onFade?.Invoke();

        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneToUnload);

        while (!operation.isDone)
        {
            yield return null;
        }
        onComplete?.Invoke();

        yield return StartCoroutine(FadeLoadingScreen(0, _fadeDurationSeconds));
        _loadingScreen.SetActive(false);
    }

    private IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = _transitionCanvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            _transitionCanvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _transitionCanvasGroup.alpha = targetValue;
    }

    public void SetFadeDuration(float duration)
    {
        _fadeDurationSeconds = duration;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
