using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timeText; // thoi gian trong game
    public GameObject pausePanel; // tam dung
    public Text pauseTimeText; // hien thi thoi gian tam dung

    private float elapsedTime = 0.0f;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText();
        }
    }
    void UpdateTimeText()
    {
        // cap nhat thoi gian da choi
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = timeString;
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            pauseTimeText.text = timeText.text;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

}
