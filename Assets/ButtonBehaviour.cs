﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject m_button;
    [SerializeField] private float m_yDelta = -4f;
    [SerializeField] private PrisonDoorBehaviour m_door;

    private bool m_down = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (m_down)
            return;

        m_button.transform.position = m_button.transform.position - Vector3.down * m_yDelta;
        m_down = true;

        m_door.Open();
    }
}