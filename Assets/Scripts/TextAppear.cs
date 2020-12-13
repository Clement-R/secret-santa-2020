using System;
using System.Collections;
using System.Collections.Generic;

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
        m_tmpText.text = p_text;
        StartCoroutine(_RevealCharacters(m_tmpText));
    }

    IEnumerator _RevealCharacters(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();

        var text = m_tmpText.text;
        textComponent.text = string.Empty;

        int index = 0;

        while (index < text.Length)
        {
            if (text[index] == '<')
            {
                textComponent.text += $"<{text[index+1]}{text[index+2]}>";
                index += 4;
                continue;
            }

            textComponent.text += text[index];

            index += 1;
            OnLetterAppear?.Invoke();

            yield return new WaitForSeconds(m_revealDelay);
        }

        yield return new WaitForSeconds(m_delayBeforeClear);

        m_tmpText.text = string.Empty;
    }
}