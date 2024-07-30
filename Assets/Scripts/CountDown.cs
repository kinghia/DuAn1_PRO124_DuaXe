using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdownText;
    public TopDownCarController playerCarController1;

    public AIController[] aiCarControllers;
    public AI2Controller[] aiCarControllers2;  // Th�m m?ng AIController2

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        // Disable car controls initially
        playerCarController1.enabled = false;
        


        foreach (var aiController in aiCarControllers)
        {
            aiController.enabled = false;
        }
        foreach (var aiController2 in aiCarControllers2)  // V� hi?u h�a AIController2
        {
            aiController2.enabled = false;
        }

        // Display countdown
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // Hide countdown text
        countdownText.text = "";

        // Enable car controls
        playerCarController1.enabled = true;
        


        foreach (var aiController in aiCarControllers)
        {
            aiController.enabled = true;
        }
        foreach (var aiController2 in aiCarControllers2)  // K�ch ho?t AIController2
        {
            aiController2.enabled = true;
        }
    }
}
