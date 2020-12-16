using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainThemeManager : MonoBehaviour
{
    public static MainThemeManager Instance;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private float m_volume = 1f;
    [SerializeField] private float m_lowVolume = 0.1f;

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

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene p_scene, LoadSceneMode p_mode)
    {
        if (p_scene.name == "InBetweenLevel")
        {
            m_audioSource.volume = m_lowVolume;
        }
        else if (p_scene.name == "Intro" || p_scene.name == "Outro" || p_scene.name == "Level1")
        {
            m_audioSource.Stop();
        }
        else if (p_scene.name.Contains("Level"))
        {
            m_audioSource.volume = m_volume;
        }
    }
}