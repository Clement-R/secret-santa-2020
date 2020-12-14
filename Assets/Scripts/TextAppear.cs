using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TextAppear : MonoBehaviour
{
    public Action OnLetterAppear;

    public bool IsTalking = false;

    [SerializeField] private TMP_Text m_tmpText;
    [SerializeField] private float m_revealDelay;
    [SerializeField] private float m_delayBeforeClear;

    public void PlayEffect(string p_text)
    {
        StartCoroutine(_RevealCharacters(m_tmpText, p_text));
    }

    IEnumerator _RevealCharacters(TMP_Text textComponent, string p_text)
    {
        IsTalking = true;

        textComponent.text = string.Empty;
        textComponent.ForceMeshUpdate();

        int index = 0;

        while (index < p_text.Length)
        {
            if (p_text[index] == '<')
            {
                textComponent.text += $"<{p_text[index+1]}{p_text[index+2]}>";
                index += 4;
                continue;
            }

            textComponent.text += p_text[index];

            index += 1;
            OnLetterAppear?.Invoke();

            yield return new WaitForSeconds(m_revealDelay);
        }

        yield return new WaitForSeconds(m_delayBeforeClear);
        textComponent.text = string.Empty;

        IsTalking = false;
    }

    [Header("Debug")]
    [SerializeField] private string m_debugText;
    [SerializeField] private float m_debugStart;
    [ContextMenu("Debug text duration")]
    private void DebugDuration()
    {
        var text = GetParsedText(m_debugText);
        var duration = ((text.Length - 1) * m_revealDelay) + m_delayBeforeClear + 0.1f;

        TimeSpan timeSpan = TimeSpan.FromSeconds(m_debugStart);
        timeSpan = timeSpan.Add(TimeSpan.FromSeconds(duration));

        Debug.Log($"Text length {text.Length}");
        Debug.Log($"Text duration {duration}");
        Debug.Log($"Text end {timeSpan}");
    }

    private string GetParsedText(string p_input)
    {
        return Regex.Replace(p_input, "<.*?>", String.Empty);
    }
}