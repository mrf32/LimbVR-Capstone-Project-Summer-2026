using System.Collections;
using UnityEngine;

public class Object : MonoBehaviour
{
    private HUDManager manager;

    [SerializeField] private GameObject doorPrefab;

    public int stayInGraspDomain = 0;

    private bool unlocking;


    private void Awake()
    {
        manager = FindFirstObjectByType<HUDManager>();

        if (manager == null)
        {
            Debug.LogError("No HUDManager found in the scene.");
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Prosthetics"))
        {
            stayInGraspDomain = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prosthetics"))
        {
            stayInGraspDomain = 0;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (unlocking || !other.CompareTag("Handle"))
        {
            return;
        }

        Animator doorAnimator = other.GetComponentInParent<Animator>();

        if (doorAnimator == null)
        {
            Debug.LogError("No Animator found on the door.");
            return;
        }


        unlocking = true;

        GameObject doorObject = doorAnimator.gameObject;

        StartCoroutine(OpenThenReplace(doorAnimator, doorObject));
    }

    private IEnumerator OpenThenReplace(
        Animator doorAnimator,
        GameObject doorObject)
    {
        if (manager != null)
        {
            manager.AddScore();
        }

        doorAnimator.SetTrigger("Open");

        /*
        Collider keyCollider = GetComponent<Collider>();

        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }
        */

        // Wait one frame so the Animator can process the trigger.
        yield return null;

        // Wait until the OpenDoor state begins.
        while (!doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            yield return null;
        }

        AnimatorStateInfo openState =
            doorAnimator.GetCurrentAnimatorStateInfo(0);

        float animationDuration =
            openState.length / Mathf.Max(doorAnimator.speed, 0.01f);

        yield return new WaitForSeconds(animationDuration);

        // Save spawn transforms before destroying the old objects.
        Vector3 doorPosition = doorObject.transform.position;
        Quaternion doorRotation = doorObject.transform.rotation;

        Vector3 keyPosition = transform.position;
        Quaternion keyRotation = transform.rotation;

        /*
        Instantiate(
            doorPrefab
        );

        Instantiate(
            keyPrefab
        );
        */


        Destroy(doorObject);
        Destroy(gameObject);
    }
    
}