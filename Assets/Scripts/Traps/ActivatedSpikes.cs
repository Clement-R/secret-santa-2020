using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActivatedSpikes : ActivationTrap
{
    [SerializeField] private float m_activationTime;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private GameObject m_spikes;
    [SerializeField] private SFX m_spikesTrigger;
    [SerializeField] private SFX m_spikesActivate;

    private float m_nextActivation = float.MinValue;
    private float m_lastActivation = float.MinValue;
    private bool m_isTriggered = false;
    private bool m_isActive = false;

    public override void TrapReset()
    {
        base.TrapReset();

        m_isActive = false;
        m_isTriggered = false;
        m_lastActivation = float.MinValue;
        m_nextActivation = float.MinValue;
    }

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (m_isTriggered)
            return;

        m_nextActivation = Time.time + m_activationTime;
        m_isTriggered = true;

        SoundsManager.Instance.PlayOneShot(m_spikesTrigger);
    }

    private void Update()
    {
        if (m_isTriggered && Time.time >= m_nextActivation)
        {
            Activate();
            m_lastActivation = Time.time;
            m_isTriggered = false;
            m_isActive = true;
        }

        if (m_isActive && Time.time >= m_lastActivation + m_activeDuration)
        {
            Deactivate();
            m_isActive = false;
        }
    }

    protected override void Activate(bool p_silent = false)
    {
        m_spikes.SetActive(true);

        if (p_silent)
            return;

        SoundsManager.Instance.PlayOneShot(m_spikesActivate);
    }

    protected override void Deactivate(bool p_silent = false)
    {
        m_spikes.SetActive(false);
    }
}