﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerHealth m_playerHealth;
    [SerializeField] private SFX m_jump;
    [SerializeField] private SFX m_land;
    [SerializeField] private SFX m_death;
    [SerializeField] private SFX m_respawn;

    void Start()
    {
        m_playerMovement.OnJump += Jump;
        m_playerMovement.OnCoyote += Jump;
        m_playerMovement.OnLand += Land;

        m_playerHealth.OnDeath += Death;

        LevelManager.Instance.OnRespawn += Respawn;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnRespawn -= Respawn;
    }

    private void Respawn(Transform p_checkpoint)
    {
        SoundsManager.Instance.PlayOneShot(m_respawn);
    }

    private void Jump()
    {
        SoundsManager.Instance.PlayOneShot(m_jump);
    }

    private void Land()
    {
        SoundsManager.Instance.PlayOneShot(m_land);
    }

    private void Death()
    {
        SoundsManager.Instance.PlayOneShot(m_death);
    }
}