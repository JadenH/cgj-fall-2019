using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cam1.enabled = false;
            cam2.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            cam1.enabled = true;
            cam2.enabled = false;
        }
    }
}