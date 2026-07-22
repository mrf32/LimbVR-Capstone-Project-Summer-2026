using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;

public class Joint1_1 : MonoBehaviour
{
    // Start is called before the first frame update
    String finger_1_msg;
    public int speed = 200;
    public float angle;
    public Prosthetics Script;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


            if (Script.isMoving ==1)
        {
            if (angle <= (60- 0.01f * speed))
            {
                angle += 0.01f * speed;
            }

            transform.localRotation = Quaternion.Euler(-1 * angle, 0f, 0f);
            // Debug.Log("The Angle is " + angle);
        }

        if (Script.isMoving == 0)
        {
            if (angle > (0+ 0.01f * speed))
            {
                angle -= 0.01f * speed;
            }

            transform.localRotation = Quaternion.Euler(-1 * angle,0f, 0f);
        }

    }
}

