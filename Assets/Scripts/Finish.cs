using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Finish : MonoBehaviour
{

    public GameObject finishLosePanel;
    public GameObject finishWinPanel; // Panel tổng kết màn chơi
    public Text winnerText; // Hiển thị tên người thắng
    public Text winnerBotText; // Hiển thị tên người thắng
    public Text playerFinishTimeText; // Hiển thị thời gian hoàn thành của người chơi
    public Text botFinishTimeText; // Hiển thị thời gian hoàn thành của người chơi
    public TopDownCarController[] botController; // Tham chiếu tới controller của người chơi
    public TopDownCarController playerController;
    public Text averageSpeedText; // Hiển thị tốc độ trung bình
    public Text averageSpeedBotText; // Hiển thị tốc độ trung bình


    private float starTime;

    private bool isFinished = false; // Để đảm bảo chỉ xử lý một lần khi về đích

    void Start()
    {
        starTime = Time.time;

        playerController = GameObject.FindGameObjectWithTag("Player2").GetComponent<TopDownCarController>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFinished) return; // Tránh xử lý nhiều lần

        if (other.CompareTag("Player1") )
        {
            isFinished = true; // Đánh dấu trò chơi đã hoàn thành

            // Lấy thời gian hoàn thành của người chơi
            float botFinishTime = Time.time - starTime;

            float totalSpeed = 0f;
            int botCount = botController.Length;

            foreach (var bot in botController)
            {
                if (bot != null) // Kiểm tra tránh lỗi null
                {
                    totalSpeed += bot.GetAverageSpeed(); // Giả sử bot có phương thức GetAverageSpeed()
                }
            }

            float averageSpeedBot = botCount > 0 ? totalSpeed / botCount : 0f;

            // Cập nhật PlayerPrefs với thời gian và tên người thắng
            PlayerPrefs.SetFloat("BotFinishTime", botFinishTime);
            PlayerPrefs.SetFloat("BotAverageSpeed", averageSpeedBot);
            PlayerPrefs.SetString("WinnerName", other.gameObject.name);

            
            
            // Hiển thị tổng kết màn chơi
            ShowFinishLosePanel();

            // Dừng tất cả âm thanh
            //AudioListener.pause = true;
            StopAllAudio();

            // Dừng game
            Time.timeScale = 0; // Dừng game khi hoàn thành
        }
        else if (other.CompareTag("Player2") )
        {
            isFinished = true; // Đánh dấu trò chơi đã hoàn thành

            // Lấy thời gian hoàn thành của người chơi
            float playerFinishTime = Time.time - starTime;

            float averageSpeedPlayer = playerController.GetAverageSpeed();

            // Cập nhật PlayerPrefs với thời gian và tên người thắng
            PlayerPrefs.SetFloat("PlayerFinishTime", playerFinishTime);
            PlayerPrefs.SetFloat("PlayerAverageSpeed", averageSpeedPlayer);
            PlayerPrefs.SetString("WinnerName", other.gameObject.name);

            
            
            // Hiển thị tổng kết màn chơi
            ShowFinishWinPanel();

            // Dừng tất cả âm thanh
            //AudioListener.pause = true;
            StopAllAudio();

            // Dừng game
            Time.timeScale = 0; // Dừng game khi hoàn thành
        }
    }

    void ShowFinishLosePanel()
    {
        // Hiển thị panel tổng kết
        finishLosePanel.SetActive(true);

        // Cập nhật thông tin người chơi
        float botFinishTime = PlayerPrefs.GetFloat("BotFinishTime", -1);
        float botAverageSpeed = PlayerPrefs.GetFloat("BotAverageSpeed", -1);

        string winnerName = PlayerPrefs.GetString("WinnerName", "No Winner");

        if (botFinishTime >= 0)
        {
            int minutes = Mathf.FloorToInt(botFinishTime / 60F);
            int seconds = Mathf.FloorToInt(botFinishTime - minutes * 60);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            botFinishTimeText.text = "FINISH TIME:" + timeString;
        }
        else
        {
            botFinishTimeText.text = "FINISH TIME: N/A";
        }

        winnerBotText.text = winnerName + " Wins!";

        if (botAverageSpeed >= 0)
        {
            averageSpeedBotText.text = $"{botAverageSpeed:F2} km/h";
        }
        else
        {
            averageSpeedBotText.text = " N/A";
        }
    }
    void ShowFinishWinPanel()
    {
        finishWinPanel.SetActive(true);

        // Cập nhật thông tin người chơi
        float playerFinishTime = PlayerPrefs.GetFloat("PlayerFinishTime", -1);
        float playerAverageSpeed = PlayerPrefs.GetFloat("PlayerAverageSpeed", -1);

        string winnerName = PlayerPrefs.GetString("WinnerName", "No Winner");

        if (playerFinishTime >= 0)
        {
            int minutes = Mathf.FloorToInt(playerFinishTime / 60F);
            int seconds = Mathf.FloorToInt(playerFinishTime - minutes * 60);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            playerFinishTimeText.text = "FINISH TIME:" + timeString;
        }
        else
        {
            playerFinishTimeText.text = "FINISH TIME: N/A";
        }

        winnerText.text = winnerName + " Wins!";

        if (playerAverageSpeed >= 0)
        {
            averageSpeedText.text = $"{playerAverageSpeed:F2} km/h";
        }
        else
        {
            averageSpeedText.text = "AVERAGE SPEED: N/A";
        }
    }

    public void ReplayRace()
    {
        Time.timeScale = 1; // Đảm bảo game tiếp tục khi chơi lại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        starTime = Time.time;
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1; // Đảm bảo game tiếp tục khi qua màn
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void StopAllAudio()
    {
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Stop();
        }
        AudioListener.pause = true; // Đảm bảo không phát âm thanh mới
    }

    public void GoToMap1()
    {
        SceneManager.LoadScene("Map 1");
    }
    public void GoToMap2()
    {
        SceneManager.LoadScene("Map 2");
    }
    public void GoToMap3()
    {
        SceneManager.LoadScene("Map 3");
    }
    public void GoToMap4()
    {
        SceneManager.LoadScene("Map 4");
    }
    public void GoToMap5()
    {
        SceneManager.LoadScene("Map 5");
    }
}