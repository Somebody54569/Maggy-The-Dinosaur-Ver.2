using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    void Update()
    {
        if (Time.timeScale > 0)
        {
            transform.Rotate(0, rotateSpeed, 0, Space.World);
        }
    }
}
