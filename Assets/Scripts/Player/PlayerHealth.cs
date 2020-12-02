using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private string m_trapLayerName = "Trap";
    [SerializeField] private float m_invincibilityDuration = 0.2f;

    private float m_lastDamageTaken = 0f;

    private void TakeDamage()
    {
        if (Time.time <= m_lastDamageTaken + m_invincibilityDuration)
        {
            return;
        }

        m_lastDamageTaken = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D p_other)
    {
        if (IsATrap(p_other))
        {
            TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D p_other)
    {
        if (IsATrap(p_other))
        {
            TakeDamage();
        }
    }

    private bool IsATrap(Collider2D p_collider)
    {
        return p_collider.gameObject.layer == LayerMask.NameToLayer(m_trapLayerName);
    }
}