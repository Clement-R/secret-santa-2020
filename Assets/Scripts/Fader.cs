using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_fadeParameter = "Fade";
    [SerializeField] private string m_doFadeParameter = "DoFade";

    public void FadeIn()
    {
        m_animator.SetTrigger(m_doFadeParameter);
        m_animator.SetBool(m_fadeParameter, true);
    }

    public void FadeOut()
    {
        m_animator.SetTrigger(m_doFadeParameter);
        m_animator.SetBool(m_fadeParameter, false);
    }
}