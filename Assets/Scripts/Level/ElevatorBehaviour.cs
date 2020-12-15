using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class ElevatorBehaviour : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_frontTrigger;
    [SerializeField] private BoxCollider2D m_insideTrigger;
    [SerializeField] private List<GameObject> m_doorParts;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private float m_seqInterval = 0.15f;

    private Sequence m_openSequence;
    private Sequence m_closeSequence;

    private PlayerMovement m_player;

    private bool m_open = false;

    void Start()
    {
        m_player = PlayerInstance.Instance.GetComponent<PlayerMovement>();

        // Open
        m_openSequence = DOTween.Sequence();

        m_openSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StartPlaying();
            }
        );

        foreach (GameObject part in m_doorParts)
        {
            m_openSequence.AppendInterval(m_seqInterval);
            m_openSequence.AppendCallback(
                () =>
                {
                    part.SetActive(!part.activeInHierarchy);
                    m_audioSource.Play();
                }
            );
        }

        m_openSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StopPlaying();
            }
        );

        m_openSequence.SetAutoKill(false);

        // Close
        m_closeSequence = DOTween.Sequence();

        m_closeSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StartPlaying();
            }
        );

        foreach (GameObject part in m_doorParts)
        {
            m_closeSequence.PrependCallback(
                () =>
                {
                    part.SetActive(!part.activeInHierarchy);
                    m_audioSource.Play();
                }
            );
            m_closeSequence.PrependInterval(m_seqInterval);
        }

        m_closeSequence.AppendCallback(
            () =>
            {
                CinematicManager.Instance.StopPlaying();
                SceneLoader.Instance.LoadScene("InBetweenLevel");
            }
        );

        m_closeSequence.SetAutoKill(false);
    }

    private void Open()
    {
        m_doorParts.ForEach(p => p.SetActive(true));
        m_openSequence.Play();

        m_open = true;
    }

    private void Close()
    {
        m_doorParts.ForEach(p => p.SetActive(false));
        m_closeSequence.Play();

        m_open = false;
    }

    private void Update()
    {
        if (m_frontTrigger.bounds.Contains(m_player.transform.position))
        {
            if (m_player.Grounded && !m_open)
            {
                Open();
            }
        }

        if (m_insideTrigger.bounds.Contains(m_player.transform.position))
        {
            if (m_player.Grounded && m_open)
            {
                Close();
            }
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD 
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_player.transform.position = m_frontTrigger.transform.position;
        }
#endif
    }
}