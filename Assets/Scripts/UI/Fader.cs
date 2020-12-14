using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader Instance;
    public float Duration => m_fadeAnimationClip.length;

    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_fadeParameter = "Fade";
    [SerializeField] private string m_doFadeParameter = "DoFade";
    [SerializeField] private AnimationClip m_fadeAnimationClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn()
    {
        m_animator.SetBool(m_fadeParameter, true);
        m_animator.SetTrigger(m_doFadeParameter);
    }

    public void FadeOut()
    {
        m_animator.SetBool(m_fadeParameter, false);
        m_animator.SetTrigger(m_doFadeParameter);
    }
}