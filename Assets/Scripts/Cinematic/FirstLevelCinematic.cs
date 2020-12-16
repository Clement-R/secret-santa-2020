using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FirstLevelCinematic : MonoBehaviour
{
    [SerializeField] private DialogPlayer m_dialogPlayer;

    private GameObject m_player;

    private void Start()
    {
        m_player = PlayerInstance.Instance.gameObject;
    }

    public void StartPlaying()
    {
        MainThemeManager.Instance.Stop();

        CinematicManager.Instance.StartPlaying();
        Invoke("CameraReset", 0.25f);
    }

    private void CameraReset()
    {
        CameraBehaviour.Instance.transform.position = new Vector3(0f, 16f, -16f);
    }

    public void StopPlaying()
    {
        CinematicManager.Instance.StopPlaying();
        MainThemeManager.Instance.Play();
    }

    public void PlayDialog(Dialog p_dialog)
    {
        m_dialogPlayer.PlayDialog(p_dialog);
    }
}