using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_menuControlGroup;
    [SerializeField] private CanvasGroup m_creditsControlGroup;
    [SerializeField] private Vector3 m_basePosition = new Vector3(0f, 0f, -16f);
    [SerializeField] private Vector3 m_creditsPosition = new Vector3(-320f, 0f, -16f);

    [SerializeField] private float m_scrollSpeed = 0.5f;

    private bool m_showingCredits = false;

    private void Start()
    {
        CameraBehaviour.Instance.transform.position = m_basePosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_showingCredits)
                SceneLoader.Instance.LoadScene("Intro");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!m_showingCredits)
                GoToCredits();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (m_showingCredits)
                GoToMenu();
        }
    }

    public void GoToCredits()
    {
        CameraBehaviour.Instance.transform
            .DOMoveX(m_creditsPosition.x, 1f, true)
            .OnStart(
                () =>
                {
                    m_menuControlGroup.alpha = 0f;
                }
            )
            .OnComplete(
                () =>
                {
                    m_showingCredits = true;
                    m_creditsControlGroup.alpha = 1f;
                }
            );
    }

    public void GoToMenu()
    {
        CameraBehaviour.Instance.transform
            .DOMoveX(m_basePosition.x, 1f, true)
            .OnStart(
                () =>
                {
                    m_creditsControlGroup.alpha = 0f;
                }
            )
            .OnComplete(
                () =>
                {
                    m_showingCredits = false;
                    m_menuControlGroup.alpha = 1f;
                }
            );
    }
}