using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    [SerializeField] private string m_playerLayerName;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_animatorOnParameter = "On";

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        if (p_collider.gameObject.layer == LayerMask.NameToLayer(m_playerLayerName))
        {
            m_animator.SetBool(m_animatorOnParameter, true);
        }
    }
}