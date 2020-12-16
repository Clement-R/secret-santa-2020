using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        CameraBehaviour.Instance.transform.position = new Vector3(0f, 0f, -16f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneLoader.Instance.LoadScene("Intro");
        }
    }
}