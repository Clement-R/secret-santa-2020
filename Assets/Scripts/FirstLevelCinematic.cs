using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FirstLevelCinematic : MonoBehaviour
{
    [Header("Text effect")]
    [SerializeField] private TextAppear m_textAppear;
    private GameObject m_player;

    private void Start()
    {
        m_player = PlayerInstance.Instance.gameObject;
    }

    public void StartPlaying()
    {
        CinematicManager.Instance.StartPlaying();
    }

    public void StopPlaying()
    {
        CinematicManager.Instance.StopPlaying();
    }

    public void PlayDialog(string p_text)
    {
        m_textAppear.PlayEffect(p_text);
    }
}