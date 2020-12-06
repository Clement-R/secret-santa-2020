using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

using TMPro;

public class ChronometerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    private Stopwatch m_stopWatch;
    private TimeSpan m_ts;

    void Start()
    {
        m_stopWatch = new Stopwatch();
        m_stopWatch.Start();

        LevelManager.Instance.OnRespawn += ResetTimer;
    }

    private void ResetTimer(Transform p_checkpoint)
    {
        m_stopWatch.Reset();
    }

    void Update()
    {
        m_ts = m_stopWatch.Elapsed;
        string elapsedTime = String.Format(
            "{0:00}:{1:00}:{2:00}",
            m_ts.Minutes,
            m_ts.Seconds,
            m_ts.Milliseconds / 10
        );
        m_text.text = elapsedTime;
    }
}