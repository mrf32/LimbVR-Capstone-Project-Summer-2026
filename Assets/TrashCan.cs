using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public bool InTrashCan = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            InTrashCan = true;

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            InTrashCan = false;
        }

    }


}