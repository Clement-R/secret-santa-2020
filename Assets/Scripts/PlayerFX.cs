using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private GameObject m_jumpingFxPrefab;
    [SerializeField] private GameObject m_landingFxPrefab;

    private void Start()
    {
        m_playerMovement.OnJump += Jump;
        m_playerMovement.OnLand += Land;

        SimplePool.Preload(m_jumpingFxPrefab, 5);
        SimplePool.Preload(m_landingFxPrefab, 5);
    }

    private void Land()
    {
        SimplePool.Spawn(m_jumpingFxPrefab, transform.position, transform.rotation);
    }

    private void Jump()
    {
        SimplePool.Spawn(m_landingFxPrefab, transform.position, transform.rotation);
    }
}