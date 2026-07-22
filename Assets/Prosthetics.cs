using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;

public class Prosthetics : MonoBehaviour
{
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
    public Object Script;
    public TrashCan Script2;
    public int Score =0;
    public bool graspStatusRef = false;
    public bool graspStatusChange = false;
    public string pathName = @"C:\Users\zhipe\Desktop\DataOutput.txt";
    //public GameObject dataObject;
    public float timer = 0;

    public GameObject spherePrefab;

    public Text timerText; // Reference to our Unity Text
    public Text scoreText; // Reference to our Unity Text
    public float gameTimer = 60f; //30 seconds for game timer

    // Keyboard addings
    public float sspeed = 10.0f;
    public float rotationSpeed = 10.0f;

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
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //Debug.Log(translation);
        //Debug.Log(rotation);

        gameTimer -= Time.deltaTime;
        timer += Time.deltaTime;
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
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        target.transform.Translate(translation, 0, 0);

        // Rotate around our y-axis
        // transform.Rotate(0, rotation, 0);
        target.transform.Translate(0, rotation, 0);
        

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

        if (isMoving == 1 & Script.stayInGraspDomain == 1 & !Script2.InTrashCan)
        {
            graspStatus = true;
            //robot.transform.position = Vector3.MoveTowards(robot.transform.position, target.transform.position + new Vector3(-0.01f, 0.060f, 0.2f), speed);
        }
        else
        {
            graspStatus = false;
        }

        if (isMoving == 1 & Script.stayInGraspDomain == 1) 
        {
            robot.transform.position = Vector3.MoveTowards(robot.transform.position, target.transform.position + new Vector3(-0.01f,0.060f,0.2f), speed);
        }

        if (Script2.InTrashCan & vlxFloat4 < 60 & gameTimer > 0f)
        {
            robot.transform.position = new Vector3(Random.Range(-1.0f,2.0f), -0.05f, Random.Range(0.5f,-0.5f));
            Score += 1;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-0.5f, 0.2f), 0, Random.Range(1.06f, 1.75f));
            Instantiate(spherePrefab, randomSpawnPosition, Quaternion.identity);
        }

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


    }
    void writeData()
    {
        using (StreamWriter file = new StreamWriter(pathName, true))
        {
            string output = string.Format("{0},{1},{2},{3}", timer, vlxFloat3,vlxFloat4, Score);
            file.WriteLine(output);
        }
    }
}