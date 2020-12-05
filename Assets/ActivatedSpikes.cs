using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActivatedSpikes : Trap
{
    [SerializeField] private float m_activationTime;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private GameObject m_spikes;

    private float m_nextActivation = float.MinValue;
    private float m_lastActivation = float.MinValue;
    private bool m_isTriggered = false;
    private bool m_isActive = false;

    public override void TrapReset()
    {
        Deactivate();
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

    private void Activate()
    {
        m_spikes.SetActive(true);
    }

    private void Deactivate()
    {
        m_spikes.SetActive(false);
    }
}