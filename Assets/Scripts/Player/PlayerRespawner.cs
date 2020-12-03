using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.OnRespawn += Respawn;
    }

    private void Respawn(Transform p_checkpoint)
    {
        transform.position = p_checkpoint.position;
    }
}