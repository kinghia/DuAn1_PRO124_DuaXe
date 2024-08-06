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
    public AI3Controller[] aiCarControllers3;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        foreach (var aiController in aiCarControllers)
        {
            aiController.enabled = false;
        }
        foreach (var aiController2 in aiCarControllers2)  // Vô hiệu hoá AIController2
        {
            aiController2.enabled = false;
        }
        foreach (var aiController3 in aiCarControllers3)
        {
            aiController3.enabled = false;
        }


        if(playerCarController1 == null) yield return new WaitForSeconds(0.1f);
        // Disable car controls initially
        playerCarController1.enabled = false;

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
        foreach (var aiController2 in aiCarControllers2)  // Kích hoạtt AIController2
        {
            aiController2.enabled = true;
        }
        foreach (var aiController3 in aiCarControllers3)
        {
            aiController3.enabled = true;
        }
    }
}
