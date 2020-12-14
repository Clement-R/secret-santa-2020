using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class DialogPlayer : MonoBehaviour
{
    public bool IsPlaying
    {
        get;
        private set;
    }

    [SerializeField] private TextAppear m_textAppear;
    [SerializeField] private SpriteRenderer m_dialogBox;
    [SerializeField] private float m_delayBetweenText = 0.1f;

    public void PlayDialog(Dialog p_dialog)
    {
        StartCoroutine(_PlayDialog(p_dialog));
    }

    private IEnumerator _PlayDialog(Dialog p_dialog)
    {
        IsPlaying = true;

        // Show dialog box
        var show = DOTween.To(() => m_dialogBox.size, x => m_dialogBox.size = x, new Vector2(140f, 140f), 1);

        yield return show.WaitForCompletion();

        foreach (var text in p_dialog.Texts)
        {
            m_textAppear.PlayEffect(text);
            while (m_textAppear.IsTalking)
            {
                yield return null;
            }

            yield return new WaitForSeconds(m_delayBetweenText);
        }

        // Hide dialog box
        var hide = DOTween.To(() => m_dialogBox.size, x => m_dialogBox.size = x, new Vector2(0f, 0f), 1);
        yield return hide.WaitForCompletion();

        IsPlaying = false;
    }
}