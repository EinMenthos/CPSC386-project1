using TMPro;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public TMP_Text clockText;
    private bool clockRunning = false;
    public float elapsedTime = 0f;

    void Start(){
        if(PlayerPrefs.HasKey("TimeBattleActual")){
            Time.timeScale = 1;

            elapsedTime = PlayerPrefs.GetFloat("TimeBattleActual");
            if (elapsedTime != 0){
                PlayerPrefs.SetFloat("TimeBattleActual", 0f);
                Debug.Log("restoring previous score: " + elapsedTime);
                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
                clockText.text = timeText;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown && !clockRunning)
        {
            // Start the clock
            clockRunning = true;
        }

        if (clockRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateClockText();
        }
    }

    void UpdateClockText()
    {
        // Format the elapsed time as minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        clockText.text = timeText;
    }
    public float ReturnClock(){
        return elapsedTime;
    }
}
