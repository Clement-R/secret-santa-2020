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
    [SerializeField] private Vector2 m_dialogBoxSize = new Vector2(140f, 140f);
    [SerializeField] private float m_delayBetweenText = 0.1f;

    public void PlayDialog(Dialog p_dialog)
    {
        StartCoroutine(_PlayDialog(p_dialog));
    }

    private IEnumerator _PlayDialog(Dialog p_dialog)
    {
        IsPlaying = true;

        // Show dialog box
        var show = DOTween.To(() => m_dialogBox.size, x => m_dialogBox.size = x, m_dialogBoxSize, 1);
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
        var hide = DOTween.To(() => m_dialogBox.size, x => m_dialogBox.size = x, Vector2.zero, 1);
        yield return hide.WaitForCompletion();

        IsPlaying = false;
    }

    [SerializeField] private Dialog m_debugDialog;
    [ContextMenu("Get Length")]
    private void GetDialogLength()
    {
        var duration = 2f; // Tweens duration
        foreach (var text in m_debugDialog.Texts)
        {
            duration += m_textAppear.GetDebugDuration(text);
            duration += m_delayBetweenText;
        }

        Debug.Log(duration);
    }
}