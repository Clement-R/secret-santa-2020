using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager Instance;
    public bool IsPlaying
    {
        get;
        private set;
    }

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

    public void StartPlaying()
    {
        IsPlaying = true;
    }

    public void StopPlaying()
    {
        IsPlaying = false;
    }
}