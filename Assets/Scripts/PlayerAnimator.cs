using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public string Jumping = "Jumping";
    public string Falling = "Falling";
    public string Walking = "Walking";

    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private Animator m_animator;

    private void Update()
    {
        m_animator.SetBool(Jumping, m_playerMovement.Jumping);
        m_animator.SetBool(Falling, m_playerMovement.Falling);
        m_animator.SetBool(Walking, m_playerMovement.Walking);

        var angle = m_playerMovement.Direction == 1 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}