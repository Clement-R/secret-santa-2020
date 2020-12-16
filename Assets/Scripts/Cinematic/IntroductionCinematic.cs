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

    public void ExclamationPoint()
    {
        var seq = DOTween.Sequence();

        seq.AppendCallback(
            () =>
            {
                m_exclamationPoint.SetActive(true);
            }
        );
        seq.Append(
            m_exclamationPoint.transform.DOJump(
                m_exclamationPoint.transform.position,
                m_jumpPower,
                1,
                m_jumpDuration,
                true
            )
        );
        seq.AppendInterval(0.1f);
        seq.AppendCallback(
            () =>
            {
                m_exclamationPoint.SetActive(false);
            }
        );

        seq.Play();
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