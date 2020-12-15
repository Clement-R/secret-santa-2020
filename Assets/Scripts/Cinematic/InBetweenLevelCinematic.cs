using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class InBetweenLevelCinematic : MonoBehaviour
{
    public static InBetweenLevelCinematic Instance;

    [SerializeField] private Vector3 m_cameraPosition;
    [SerializeField] private Dialog[] m_dialogs;
    private DialogPlayer m_dialogPlayer;

    private int m_index = 0;
    private int m_sceneIndex = 1;

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
        var scene = SceneManager.GetActiveScene();
        if (scene.name == "InBetweenLevel")
        {
            StartCinematic();
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }

    private IEnumerator _Cinematic()
    {
        yield return new WaitForSeconds(1f);

        if (m_index < m_dialogs.Length)
        {
            m_dialogPlayer = FindObjectOfType<DialogPlayer>();
            m_dialogPlayer.PlayDialog(m_dialogs[m_index]);
            m_index++;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        m_sceneIndex++;

        while (m_dialogPlayer.IsPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        int sceneIndex = SceneUtility.GetBuildIndexByScenePath($"Scenes/Level{m_sceneIndex}");
        if (sceneIndex >= 0)
        {
            SceneLoader.Instance.LoadScene($"Level{m_sceneIndex}");
        }
        else
        {
            SceneLoader.Instance.LoadScene("Outro");
        }
    }

    private void SceneLoaded(Scene p_scene, LoadSceneMode p_sceneMode)
    {
        if (p_scene.name != "InBetweenLevel")
        {
            return;
        }

        StartCinematic();
    }

    private void StartCinematic()
    {
        CameraBehaviour.Instance.transform.position = m_cameraPosition;
        StartCoroutine(_Cinematic());
    }
}