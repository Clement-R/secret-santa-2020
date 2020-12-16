using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    [SerializeField] private Canvas m_canvas;

    private void Start()
    {
        m_canvas.worldCamera = CameraBehaviour.Instance.gameObject.GetComponent<Camera>();
    }
}