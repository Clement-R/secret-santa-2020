using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action OnJump;
    public Action OnLand;

    public bool Jumping => m_rb.velocity.y > 0.1f;
    public bool Falling => m_rb.velocity.y < -0.1f;
    public bool Walking => m_rb.velocity.x != 0f;
    public int Direction => m_rb.velocity.x > 0 ? 1 : -1;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private float m_speed;

    [Header("Jump")]
    [SerializeField] private float m_jumpHeight;

    [Header("Grounded")]
    [SerializeField] private string m_obstacleLayer = "Obstacle";
    [SerializeField] private float m_groundedDistance = 2f;

    [Header("Coyote time")]
    [SerializeField] private float m_coyoteTime = 0.25f;

    [Header("Debug")]
    [SerializeField] private bool m_debug = false;

    private float m_forward = 0f;
    private float m_jump = 0f;
    private bool m_grounded = true;
    private float m_lastGrounded = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        GetInputs();

        // Check if grounded
        var grounded = IsGrounded();
        if (m_grounded != grounded)
        {
            m_grounded = grounded;
            OnLand?.Invoke();
        }

        if (m_grounded)
        {
            m_lastGrounded = Time.time;
        }

        if (m_debug)
        {
            VisualDebug();
        }
    }

    private bool CanJump()
    {
        // Coyote time
        if (!m_grounded && Time.time <= m_lastGrounded + m_coyoteTime)
        {
            return true;
        }

        return m_grounded;
    }

    private void GetInputs()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_forward = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_forward = -1f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            m_jump = 1f;
        }
    }

    private void VisualDebug()
    {
        // Debug grounded
        if (m_grounded)
        {
            Debug.DrawLine(
                transform.position,
                transform.position - transform.up * m_groundedDistance,
                Color.green
            );
        }
        else
        {
            Debug.DrawLine(
                transform.position,
                transform.position - transform.up * m_groundedDistance,
                Color.red
            );
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.Raycast(
            transform.position,
            transform.up * -1f,
            m_groundedDistance,
            LayerMask.GetMask(m_obstacleLayer)
        );
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        if (m_forward != 0f)
        {
            m_rb.velocity = new Vector2(m_forward * m_speed, m_rb.velocity.y);
        }
        else
        {
            m_rb.velocity = new Vector2(0f, m_rb.velocity.y);
        }

        if (m_jump != 0f && CanJump())
        {
            m_rb.AddForce(new Vector2(0f, m_jumpHeight), ForceMode2D.Impulse);
            OnJump?.Invoke();
        }

        m_forward = 0f;
        m_jump = 0f;
    }
}