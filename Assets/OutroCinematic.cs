using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OutroCinematic : MonoBehaviour
{
    [SerializeField] private Vector3 m_cameraPosition;
    [SerializeField] private DialogPlayer m_dialogPlayer;

    private void Start()
    {
        CameraBehaviour.Instance.transform.position = m_cameraPosition;
    }

    public void PlayDialog(Dialog p_dialog)
    {
        m_dialogPlayer.PlayDialog(p_dialog);
    }

    public void LoadMenuScene()
    {
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}