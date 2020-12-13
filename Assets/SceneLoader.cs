using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Fader m_fader;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string p_sceneName)
    {
        StartCoroutine(_LoadScene(p_sceneName));
    }

    IEnumerator _LoadScene(string p_sceneName)
    {
        var start = Time.time;
        m_fader.FadeIn();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(p_sceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (Time.time <= start + m_fader.Duration + 0.05f)
        {
            yield return null;
        }

        Time.timeScale = 0f;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        yield return new WaitForSecondsRealtime(1.5f);

        m_fader.FadeOut();

        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(m_fader.Duration + 0.05f);
    }
}