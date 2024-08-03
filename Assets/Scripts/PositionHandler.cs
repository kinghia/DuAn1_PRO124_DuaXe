using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PositionHandler : MonoBehaviour
{
    LeaderboardUIHandler leaderboardUIHandler;
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    public void Awake()
    {
        UpdatePositionHandler();
    }

    public void UpdatePositionHandler()
    {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        carLapCounters = carLapCounterArray.ToList<CarLapCounter>(); 

        foreach (CarLapCounter lapCounters in carLapCounters)
            lapCounters.OnPassCheckpoint += OnPassCheckpoint;

        leaderboardUIHandler = FindObjectOfType<LeaderboardUIHandler>();
    }
    void Start()
    {
        leaderboardUIHandler.UpdateList(carLapCounters);
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckpointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        int carPosition = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPosition(carPosition);

        leaderboardUIHandler.UpdateList(carLapCounters);
    }

}
