using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgainMenu : MonoBehaviour
{
    public string nextSceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
