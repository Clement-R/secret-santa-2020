using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    public Action<CheckpointBehaviour> OnActivation;

    [SerializeField] private string m_playerLayerName;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_animatorOnParameter = "On";
    [SerializeField] private SFX m_activateSFX;

    private bool m_isActive = false;

    private void Start()
    {
        LevelManager.Instance.RegisterCheckpoint(this);
    }

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.layer == LayerMask.NameToLayer(m_playerLayerName))
        {
            Activate();
        }
    }

    private void Activate()
    {
        if (m_isActive)
            return;

        m_isActive = true;
        m_animator.SetBool(m_animatorOnParameter, true);
        SoundsManager.Instance.PlayOneShot(m_activateSFX);
        OnActivation?.Invoke(this);
    }
}