using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class IntroductionCinematic : MonoBehaviour
{
    [Header("Text effect")]
    [SerializeField] private TextAppear m_textAppear;
    [SerializeField] private DialogPlayer m_dialogPlayer;

    [Header("Exclamation point")]
    [SerializeField] private GameObject m_exclamationPoint;
    [SerializeField] private float m_jumpPower = 25f;
    [SerializeField] private float m_jumpDuration = 0.25f;

    private Sequence m_sequence;

    private void Start()
    {
        CameraBehaviour.Instance.transform.position = new Vector3(0f, 0f, -16f);

        m_sequence = DOTween.Sequence();

        m_sequence.AppendCallback(
            () =>
            {
                m_exclamationPoint.SetActive(true);
            }
        );
        m_sequence.Append(
            m_exclamationPoint.transform.DOJump(
                m_exclamationPoint.transform.position,
                m_jumpPower,
                1,
                m_jumpDuration,
                true
            )
        );
        m_sequence.AppendInterval(0.1f);
        m_sequence.AppendCallback(
            () =>
            {
                m_exclamationPoint.SetActive(false);
            }
        );
    }

    public void ExclamationPoint()
    {
        m_sequence.Play();
    }

    public void PlayDialog(Dialog p_dialog)
    {
        m_dialogPlayer.PlayDialog(p_dialog);
    }

    public void LoadGameScene()
    {
        SceneLoader.Instance.LoadScene("Level1");
    }
}