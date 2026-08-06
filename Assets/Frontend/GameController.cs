using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Prosthetics prosthetic;

    private Vector3 initialHandPosition;
    private Quaternion initialHandRotation;

    GameObject[] keys;
    GameObject[] doors;



    private void Awake()
    {
        prosthetic = FindFirstObjectByType<Prosthetics>();
        if (hand == null)
        {
            Debug.LogError("Hand is not assigned in GameController.");
            return;
        }

        initialHandPosition = hand.transform.position;
        initialHandRotation = hand.transform.rotation;
    }

    public void Update()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        doors = GameObject.FindGameObjectsWithTag("Door");
        if (doors.Length == 0)
        {
            ResetPlay();
        }


    }

    public void ResetHand()
    {
        if (hand == null)
            return;

        hand.transform.SetPositionAndRotation(
            initialHandPosition,
            initialHandRotation
        );
    }

    public void GenerateNewKey()
    {
        if (keys.Length > 1)
        {
            foreach (GameObject key in keys)
            {
                Destroy(key);
            }

        }
        else if (keys.Length == 0)
        {
            if (keyPrefab == null)
            {
                Debug.LogError("Key prefab is not assigned.");
                return;
            }
            //assign current copy to prosthetic
            Instantiate(keyPrefab);
            //AssignToProsthetic();
        }
    }

    public GameObject AssignToProsthetic()
    {
        var currentKey = Instantiate(keyPrefab);
        prosthetic.robot = currentKey;
        prosthetic.Object = currentKey.GetComponent<Object>();
        return currentKey;
    }

    public void GenerateNewDoor()
    {
        if(doors.Length > 1)
        {
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }

        } else if(doors.Length == 0)
        {
            if (doorPrefab == null)
            {
                Debug.LogError("Door prefab is not assigned.");
                return;
            }

            Instantiate(doorPrefab);
        }
    }

    public void ResetPlay()
    {
        //Instantiate(keyPrefab);
        AssignToProsthetic();
        Instantiate(doorPrefab);
        ResetHand();
    }
}