using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TalkingVoice : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_voiceClips;
    [SerializeField] private Vector2 m_randomPauseRange;
    [SerializeField] private Vector2 m_randomPitchRange;

    private bool m_isTalking = false;
    private float m_next = 0f;

    void Update()
    {
        m_isTalking = Input.GetKey(KeyCode.D);

        if (m_isTalking)
        {
            if (Time.time > m_next)
            {
                var clip = m_voiceClips[Random.Range(0, m_voiceClips.Length)];
                m_audioSource.PlayOneShot(clip);
                m_audioSource.pitch = Random.Range(m_randomPitchRange.x, m_randomPitchRange.y);

                m_next = Time.time + clip.length + Random.Range(m_randomPauseRange.x, m_randomPauseRange.y);
            }
        }
    }
}