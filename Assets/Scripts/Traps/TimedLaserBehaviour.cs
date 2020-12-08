using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimedLaserBehaviour : ActivationTrap
{
    [SerializeField] private float m_delayDuration;
    [SerializeField] private float m_offsetDuration;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private float m_previewDuration;
    [SerializeField] private SpriteRenderer m_laser;
    [SerializeField] private Sprite m_laserSprite;
    [SerializeField] private Sprite m_laserPreviewSprite;
    [SerializeField] private SFX m_laserPreviewSFX;
    [SerializeField] private SFX m_laserActivateSFX;
    [SerializeField] private Collider2D m_collider;

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
        if (Time.time > m_nextActivation - m_previewDuration)
        {
            PreActivate();
        }

        if (Time.time > m_nextActivation)
        {
            Activate();
        }

        if (Time.time > m_lastActivation + m_activeDuration)
        {
            Deactivate();
        }
    }

    private void PreActivate()
    {
        m_laser.sprite = m_laserPreviewSprite;

        // SoundsManager.Instance.PlayOneShot(m_laserPreviewSFX);
    }

    protected override void Activate(bool p_silent = false)
    {
        m_collider.enabled = true;

        m_laser.sprite = m_laserSprite;
        m_lastActivation = Time.time;
        m_nextActivation = m_lastActivation + m_activeDuration + m_delayDuration;

        // SoundsManager.Instance.PlayOneShot(m_laserActivateSFX);
    }

    protected override void Deactivate(bool p_silent = false)
    {
        m_collider.enabled = false;
        m_laser.sprite = null;
    }
}