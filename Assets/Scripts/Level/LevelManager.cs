using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Action<Transform> OnRespawn;

    private CheckpointBehaviour m_lastCheckpoint;
    private List<Trap> m_traps = new List<Trap>();

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
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void SceneLoaded(Scene p_scene, LoadSceneMode p_sceneMode)
    {

    }

    private void SceneUnloaded(Scene arg0)
    {
        m_traps.Clear();
    }

    public void RegisterTrap(Trap p_trap)
    {
        m_traps.Add(p_trap);
    }

    public void RegisterCheckpoint(CheckpointBehaviour p_checkpoint)
    {
        p_checkpoint.OnActivation += CheckpointActivated;
    }

    private void CheckpointActivated(CheckpointBehaviour p_checkpoint)
    {
        m_lastCheckpoint = p_checkpoint;
    }

    public void Respawn()
    {
        OnRespawn?.Invoke(m_lastCheckpoint.transform);
        m_traps.ForEach(t => t.TrapReset());
    }
}