using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;

public class Joint1_2 : MonoBehaviour
{
    // Start is called before the first frame update
    String finger_1_msg;
    public int speed = 100;
    public float angle;
    public Prosthetics script;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        angle = script.vlxFloat3;
        transform.localRotation = Quaternion.Euler(-1 * angle, -10f, 0f);

    }
}

