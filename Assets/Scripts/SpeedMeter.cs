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
        if (carController != null && speedText != null)
        {
            // Lấy tốc độ trung bình từ carController
            float averageSpeed = carController.GetAverageSpeed();

            // Hiển thị tốc độ trung bình
            speedText.text = " " + averageSpeed.ToString("F2") + " km/h";
        }
    }
}