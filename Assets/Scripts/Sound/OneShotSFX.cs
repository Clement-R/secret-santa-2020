using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OneShotSFX : MonoBehaviour
{
    [SerializeField] private AudioSource m_source;

    public void Play(SFX p_sfx)
    {
        m_source.PlayOneShot(p_sfx.Clip, p_sfx.Volume);
        Invoke("SelfDestroy", p_sfx.Clip.length + 0.1f);
    }

    private void SelfDestroy()
    {
        SimplePool.Despawn(gameObject);
    }
}