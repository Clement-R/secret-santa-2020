using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class PrisonDoorBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_parts;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private GameObject m_shadow;
    [SerializeField] private Vector3 m_finalPos;
    [SerializeField] private float m_seqInterval = 0.15f;

    private Sequence m_openSequence;

    public void Open()
    {
        // Open
        m_openSequence = DOTween.Sequence();

        m_openSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StartPlaying();
            }
        );

        foreach (GameObject part in m_parts)
        {
            m_openSequence.AppendInterval(m_seqInterval);
            m_openSequence.AppendCallback(
                () =>
                {
                    part.SetActive(!part.activeInHierarchy);
                    m_audioSource.Play();
                    m_shadow.transform.position += Vector3.up * 16f;
                }
            );
        }

        m_openSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StopPlaying();
                m_shadow.transform.position = m_finalPos;
            }
        );

        m_openSequence.Play();
    }
}