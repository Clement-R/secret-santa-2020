using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private GameObject m_jumpingFxPrefab;
    [SerializeField] private GameObject m_landingFxPrefab;
    [SerializeField] private GameObject m_coyoteFxPrefab;
    [SerializeField] private ParticleSystem m_walkParticles;

    [SerializeField] private Transform m_coyoteTransform;

    private void Start()
    {
        m_playerMovement.OnJump += Jump;
        m_playerMovement.OnLand += Land;
        m_playerMovement.OnCoyote += Coyote;

        SimplePool.Preload(m_jumpingFxPrefab, 5);
        SimplePool.Preload(m_landingFxPrefab, 5);
    }

    private void Update()
    {
        if (m_playerMovement.Walking)
        {
            if (!m_walkParticles.isPlaying)
            {
                m_walkParticles.Play();
            }
        }
        else
        {
            if (m_walkParticles.isPlaying)
            {
                m_walkParticles.Stop();
            }
        }
    }

    private void Coyote()
    {
        var fx = SimplePool.Spawn(m_coyoteFxPrefab, m_coyoteTransform.position, Quaternion.identity);
        fx.transform.localScale = new Vector3(
            1 * Mathf.Sign(m_playerMovement.Velocity.x),
            fx.transform.localScale.y,
            fx.transform.localScale.z
        );
    }

    private void Land()
    {
        SimplePool.Spawn(m_landingFxPrefab, transform.position, Quaternion.identity);
    }

    private void Jump()
    {
        SimplePool.Spawn(m_jumpingFxPrefab, m_playerMovement.GroundPosition, Quaternion.identity);
    }
}