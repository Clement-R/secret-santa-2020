using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action OnDeath;

    public bool Dead
    {
        get;
        private set;
    } = false;

    [SerializeField] private string m_trapLayerName = "Trap";

    private Coroutine m_respawnRoutine = null;

    private void TakeDamage()
    {
        if (m_respawnRoutine != null)
        {
            return;
        }

        m_respawnRoutine = StartCoroutine(_Respawn());
    }

    private IEnumerator _Respawn()
    {
        Dead = true;
        OnDeath?.Invoke();

        yield return new WaitForSecondsRealtime(0.5f);

        LevelManager.Instance.Respawn();

        m_respawnRoutine = null;
        Dead = false;
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