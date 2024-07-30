using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Finish : MonoBehaviour
{

    public GameObject finishPanel; // Panel tổng kết màn chơi
    public Text winnerText; // Hiển thị tên người thắng
    public Text playerFinishTimeText; // Hiển thị thời gian hoàn thành của người chơi
    private float starTime;

    private bool isFinished = false; // Để đảm bảo chỉ xử lý một lần khi về đích

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFinished) return; // Tránh xử lý nhiều lần

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isFinished = true; // Đánh dấu trò chơi đã hoàn thành

            // Lấy thời gian hoàn thành của người chơi
            float playerFinishTime = Time.time - starTime;

            // Cập nhật PlayerPrefs với thời gian và tên người thắng
            PlayerPrefs.SetFloat("PlayerFinishTime", playerFinishTime);
            PlayerPrefs.SetString("WinnerName", other.gameObject.name);

            // Hiển thị tổng kết màn chơi
            ShowFinishPanel();

            // Dừng game
            Time.timeScale = 0; // Dừng game khi hoàn thành
        }
    }
    void Start()
    {
        starTime = Time.time;
    }

    void ShowFinishPanel()
    {
        // Hiển thị panel tổng kết
        finishPanel.SetActive(true);

        // Cập nhật thông tin người chơi
        float playerFinishTime = PlayerPrefs.GetFloat("PlayerFinishTime", -1);
        string winnerName = PlayerPrefs.GetString("WinnerName", "No Winner");

        if (playerFinishTime >= 0)
        {
            int minutes = Mathf.FloorToInt(playerFinishTime / 60F);
            int seconds = Mathf.FloorToInt(playerFinishTime - minutes * 60);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            playerFinishTimeText.text = "FINISH TIME: " + timeString;
        }
        else
        {
            playerFinishTimeText.text = "FINISH TIME: N/A";
        }

        winnerText.text = winnerName + " Wins!";
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
}