using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    //Object Variables & Controllers
    [Header("Gameplay HUD Elements")]

    public GameObject startScreenContainer;
    public GameObject gameplayHUDContainer;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    [Header("Result Screen HUD Elements")]
    public GameObject resultScreenContainer;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI backendStatsText;

    //Local variables for timer and state
    private float timeRemaining = 121f;
    private int currentScore = 0;
    private bool isGameActive = false;

    void Start()
    {
        //On startup sets the correct screen to display and reset score counter

        startScreenContainer.SetActive(true);
        gameplayHUDContainer.SetActive(false);
        resultScreenContainer.SetActive(false);

        UpdateScoreDisplay();
    }

    void Update()
    {
        /*Test code 'Space' to instantly end and 'S' to increase score
        if(isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            EndSession();
        }

        if(isGameActive && Input.GetKeyDown(KeyCode.S))
        {
            AddScore();
        }
        */

        if (!isGameActive) return; //Prevents timer from starting 

        //Timer and end of timer state change
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerDisplay(timeRemaining);
            EndSession();
        }
    }

    //Start Game Button
    public void StartGame()
    {
        isGameActive  = true;

        startScreenContainer.SetActive(false);
        gameplayHUDContainer.SetActive(true);
    }

    //Score
    public void AddScore()
    {
        if (!isGameActive) return;
        currentScore += 1;
        UpdateScoreDisplay();
    }

    //Timer function
    void UpdateTimerDisplay(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Score function
    void UpdateScoreDisplay()
    {
        scoreText.text = "Doors Unlocked: " + currentScore;
    }

    //End if game screen
    public void EndSession()
    {
        isGameActive = false;

        gameplayHUDContainer.SetActive(false);
        resultScreenContainer.SetActive(true);

        finalScoreText.text = "Final Score: " + currentScore + " Doors";

        //string mockBackendLogs = "PROSTHETIC REHAB LOGS:\n" + 
        //DisplayBackendStats()
    }

    /* Backend stats WIP not final
    public void DisplayBackendStats(string technicalLogs)
    {
        backendStatsText.text = technicalLogs;
    }
    */

    //Button Function to restart game

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


}