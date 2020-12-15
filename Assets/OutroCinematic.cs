using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OutroCinematic : MonoBehaviour
{
    [SerializeField] private Vector3 m_cameraPosition;
    private void Start()
    {
        CameraBehaviour.Instance.transform.position = m_cameraPosition;
    }
}