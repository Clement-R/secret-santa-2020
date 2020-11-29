using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jumpHeight;

    private float m_forward = 0f;
    private float m_jump = 0f;

    private void Start()
    {

    }

    private void Update()
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

        if (m_jump != 0f)
        {
            m_rb.AddForce(new Vector2(0f, m_jumpHeight), ForceMode2D.Impulse);
        }

        m_forward = 0f;
        m_jump = 0f;

        Physics2D.Raycast(transform.position, transform.up * -1f, 10f);
        //TODO: Upgrade with layer of enviro
    }
}