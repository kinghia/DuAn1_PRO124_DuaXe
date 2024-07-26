using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinishScreenManager : MonoBehaviour
{
    public Text winnerText;
    public Text playerFinishTimeText;

    public void Start()
    {
        // Lấy thời gian về đích của người chơi và tên người chiến thắng từ PlayerPrefs
        float playerFinishTime = PlayerPrefs.GetFloat("PlayerFinishTime", -1);
        string winnerName = PlayerPrefs.GetString("WinnerName", "No Winner");

        // Hiển thị thông tin người chơi
        if (playerFinishTime >= 0)
        {
            int minutes = Mathf.FloorToInt(playerFinishTime / 60F);
            int seconds = Mathf.FloorToInt(playerFinishTime - minutes * 60);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            playerFinishTimeText.text = "Thời gian hoàn thành của bạn: " + timeString;
        }
        else
        {
            playerFinishTimeText.text = "Thời gian hoàn thành của bạn: N/A";
        }

        winnerText.text = winnerName + " Wins!";
    }

    public void ReturnToMainMenu()
    {
        // Quay lại menu chính
        SceneManager.LoadScene("MainMenu");
    }
}