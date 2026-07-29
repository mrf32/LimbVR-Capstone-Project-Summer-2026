using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public int speed = 200;
    public float angle;
    public Prosthetics Script;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Script.isMoving == 1)
        {
            if (angle <= (60 - 0.01f * speed))
            {
                angle += 0.01f * speed;
            }

            transform.localRotation = Quaternion.Euler(-1 * angle, 0f, 0f);
            // Debug.Log("The Angle is " + angle);
        }

        if (Script.isMoving == 0)
        {
            if (angle > (0 + 0.01f * speed))
            {
                angle -= 0.01f * speed;
            }

            transform.localRotation = Quaternion.Euler(-1 * angle, 0f, 0f);
        }
    }
}
