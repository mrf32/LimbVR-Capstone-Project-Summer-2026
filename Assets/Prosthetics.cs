using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;

public class Prosthetics : MonoBehaviour
{
    private struct LoggingAction
    {
        public KeyCode Key;
        public string ActionName;

        public LoggingAction(KeyCode key, string actionName)
        {
            Key = key;
            ActionName = actionName;
        }
    }

    private readonly LoggingAction[] loggingActions = new LoggingAction[]
    {
        new LoggingAction(KeyCode.Space, "grasp"),
        new LoggingAction(KeyCode.W, "move up"),
        new LoggingAction(KeyCode.A, "move left"),
        new LoggingAction(KeyCode.S, "move down"),
        new LoggingAction(KeyCode.D, "move right"),
        new LoggingAction(KeyCode.UpArrow, "move up"),
        new LoggingAction(KeyCode.DownArrow, "move down"),
        new LoggingAction(KeyCode.LeftArrow, "move left"),
        new LoggingAction(KeyCode.RightArrow, "move right"),
        new LoggingAction(KeyCode.Q, "left rotation"),
        new LoggingAction(KeyCode.E, "right rotation"),
        new LoggingAction(KeyCode.F, "left rotation"),
        new LoggingAction(KeyCode.G, "right rotation")
    };

    public SerialPort serial = new SerialPort("\\\\.\\COM3", 115200);
    private string vlxstring;
    public float vlxFloat1, vlxFloat2, vlxFloat3, vlxFloat4;
    public static char[] delimiter = new char[] { ',' };
    private bool serialState;
    public string[] output;

    public GameObject robot;
    public GameObject target;
    public float speed=0.01f;
    public int isMoving = 0;
    public bool graspStatus=false;
    public Object Object;
    public TrashCan Door;//change type to gameobject
    public int Score =0;
    public bool graspStatusRef = false;
    public bool graspStatusChange = false;
    public string pathName = @"Assets/logs/DataOutput.txt";
    //public GameObject dataObject;
    public float timer = 0;

    public GameObject spherePrefab;

    public Text timerText; // Reference to our Unity Text
    public Text scoreText; // Reference to our Unity Text
    public float gameTimer = 240f; //2 mins for game timer

    // Keyboard addings
    public float sspeed = 10.0f;
    public float rotationSpeed = 0.01f;

    [Header("Visual Assignments")]
    public Transform visualMeshChild;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        serial.Open();
        Debug.Log(vlxFloat1);
        serialState = true;
        */
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard addings
        float translation_v = Input.GetAxis("Vertical") * speed;
        float translation_h = Input.GetAxis("Horizontal") * speed;
        float rotation = Input.GetAxis("Rotational") * rotationSpeed;
        //Debug.Log(translation);
        //Debug.Log(rotation);

        gameTimer -= Time.deltaTime;
        timer += Time.deltaTime;
        CheckLoggingInput();
        // Debug.Log(Score);


        /* BUG */
        // This line attempts to log some data but because specific file path from above
        // is not found this messes with rest of Update execution.
        // FIX: Leave function call commented for now until solution is found for 
        // hardcoded file path from above

        //writeData();

        if (serialState == true)
        {
            vlxstring = serial.ReadLine();
            output = vlxstring.Split(delimiter);
            float.TryParse(output[0], out vlxFloat1);
            float.TryParse(output[1], out vlxFloat2);
            float.TryParse(output[2], out vlxFloat3);
            float.TryParse(output[3], out vlxFloat4);
        }

        //Debug.Log(vlxFloat1);

        //target.transform.position = new Vector3(vlxFloat1, 0, vlxFloat2);

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation_v *= Time.deltaTime;
        translation_h *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        target.transform.Translate(0, 0, -translation_v);
        target.transform.Translate(0, translation_h, 0);

        // Rotate around our y-axis
        visualMeshChild.Rotate(0, 0, rotation);



        // Debug.Log(Score);

        if (graspStatusRef != graspStatus)
        {
            graspStatusRef = graspStatus;
            graspStatusChange = true;
        }
        else
        {
            graspStatusChange = false;
        }

        


        // if (vlxFloat4 > 90)
        if (Input.GetKey(KeyCode.Space))
        {
            isMoving = 1;
        }
        else
        {
            isMoving = 0;
        }

        if (isMoving == 1 & Object.stayInGraspDomain == 1 /*& !Door.InTrashCan*/)
        {
            graspStatus = true;
            //robot.transform.position = Vector3.MoveTowards(robot.transform.position, target.transform.position + new Vector3(-0.01f, 0.060f, 0.2f), speed);
        }
        else
        {
            graspStatus = false;
        }


        if (isMoving == 1 && Object.stayInGraspDomain == 1 && target != null) 
        {

            Transform gripAnchor = target.transform.Find("Grip_Anchor");
            
            if(gripAnchor != null){
                robot.transform.position = Vector3.MoveTowards(robot.transform.position, gripAnchor.position, speed);
            }
            else{
                robot.transform.position = Vector3.MoveTowards(robot.transform.position, target.transform.position, speed);
            }

        }

        /*
        if (Door.InTrashCan & vlxFloat4 < 60 & gameTimer > 0f) // Door variable named changed from another previous name affecting original trash bin game
        {
            robot.transform.position = new Vector3(Random.Range(-1.0f,2.0f), -0.05f, Random.Range(0.5f,-0.5f));
            //Score += 1;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-0.5f, 0.2f), 0, Random.Range(1.06f, 1.75f));
            //Instantiate(spherePrefab, randomSpawnPosition, Quaternion.identity);
        }
        */


        /*
        if (gameTimer > 0f)
        {
            timerText.text = "Time Left: " + Mathf.Floor(gameTimer);
            //Debug.Log("sdf");
        }
        else
        {
            timerText.text = "GAME OVER";
        }

        scoreText.text = "Score: " + Mathf.Floor(Score);
        */

    }

    void CheckLoggingInput()
    {
        foreach (LoggingAction action in loggingActions)
        {
            if (Input.GetKeyDown(action.Key))
            {
                writeData(action.ActionName);
                return;
            }
        }
    }

    void writeData(string actionName)
    {
        string directoryName = Path.GetDirectoryName(pathName);
        if (!string.IsNullOrEmpty(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        using (StreamWriter file = new StreamWriter(pathName, true))
        {
            string output = string.Format("{0},{1},{2},{3},{4}", timer, vlxFloat3, vlxFloat4, Score, actionName);
            file.WriteLine(output);
            file.Close();
        }
    }
}


