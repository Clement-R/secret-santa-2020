using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimedLaserBehaviour : ActivationTrap
{
    [SerializeField] private float m_delayDuration;
    [SerializeField] private float m_offsetDuration;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private GameObject m_laser;

    private float m_lastActivation = 0f;
    private float m_nextActivation = 0f;

    public override void TrapReset()
    {
        base.TrapReset();

        m_lastActivation = 0f;
        m_nextActivation = Time.time + m_offsetDuration;
    }

    void Update()
    {
        if (Time.time > m_nextActivation)
        {
            Activate();
        }

        if (Time.time > m_lastActivation + m_activeDuration)
        {
            Deactivate();
        }
    }

    protected override void Activate(bool p_silent = false)
    {
        m_laser.SetActive(true);
        m_lastActivation = Time.time;
        m_nextActivation = m_lastActivation + m_activeDuration + m_delayDuration;
    }

    protected override void Deactivate(bool p_silent = false)
    {
        m_laser.SetActive(false);
    }
}