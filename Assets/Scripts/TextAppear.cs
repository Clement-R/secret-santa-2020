using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{
    public Action OnLetterAppear;

    [SerializeField] private Text m_text;
    [SerializeField] private float m_revealDelay;

    private string m_fullText;
    private int m_index = 0;

    private float m_nextReveal = 0f;

    private void Awake()
    {
        if (m_text == null)
            return;

        m_fullText = m_text.text;
        m_text.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (Time.time > m_nextReveal && m_index < m_fullText.Length)
            {
                m_text.text += m_fullText[m_index];
                m_nextReveal = Time.time + m_revealDelay;
                m_index++;
                OnLetterAppear?.Invoke();
            }
        }
    }
}