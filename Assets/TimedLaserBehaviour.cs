using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimedLaserBehaviour : MonoBehaviour
{
    [SerializeField] private float m_delayDuration;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private GameObject m_laser;

    private float m_lastActivation = 0f;
    private float m_nextActivation = 0f;

    void Start()
    {
        m_laser.SetActive(false);
    }

    void Update()
    {
        if (Time.time > m_nextActivation)
        {
            m_laser.SetActive(true);
            m_lastActivation = Time.time;
            m_nextActivation = m_lastActivation + m_activeDuration + m_delayDuration;
        }

        if (Time.time > m_lastActivation + m_activeDuration)
        {
            m_laser.SetActive(false);
        }
    }
}