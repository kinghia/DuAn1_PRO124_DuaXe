using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    public TopDownCarController carController;
    public Text speedText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // toc do cua xe tra ve toc do don vi m/s
        float speed = carController.GetVelocityMagnitude();

        // chuyen doi toc do tu m/s sang km/h
        float speedKmh = speed * 2.5f;

        // cap nhat toc do
        speedText.text = speedKmh.ToString("F1") + " km/h";


    }
}
