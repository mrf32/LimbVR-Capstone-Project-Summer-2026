using UnityEngine;

public class Object : MonoBehaviour
{
    public int stayInGraspDomain=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(stayInGraspDomain);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Prosthetics")
        {
            stayInGraspDomain = 1;

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prosthetics")
        {
            stayInGraspDomain = 0;



        }

    }


}
