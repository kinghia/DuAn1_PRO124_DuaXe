using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinishScreenManager : MonoBehaviour
{
    public GameObject finishPanel;  // Panel tổng kết màn chơi

    public Button replayButton;  // Nút chơi lại
    public Button nextLevelButton;  // Nút qua màn

    private void Start()
    {
        replayButton.onClick.AddListener(ReplayRace);
        nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    public void ReplayRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}