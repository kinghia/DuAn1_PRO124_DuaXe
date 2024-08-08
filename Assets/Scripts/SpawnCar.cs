using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        CarData[] carDatas = Resources.LoadAll<CarData>("CarData/");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i].transform;

            int PlayerSelectedCardID = PlayerPrefs.GetInt($"P{i + 1}SelectedCarID");

            foreach (CarData cardata in carDatas)
            {
                if (cardata.CarUniqueID == PlayerSelectedCardID)
                {
                    GameObject playerCar = Instantiate(cardata.CarPrefab, spawnPoint.position, spawnPoint.rotation);

                    playerCar.GetComponent<CarInputHandler>().playerNumber = i + 1;
                    gameObject.GetComponent<CameraFollow>().target = playerCar.transform;
                    GameObject.Find("Leadboard Canvas").GetComponent<LeaderboardUIHandler>().UpdateLeaderBoard();
                    gameObject.GetComponent<PositionHandler>().UpdatePositionHandler();
                    GameObject.Find("CountDown").GetComponent<CountDown>().playerCarController1 = playerCar.GetComponent<TopDownCarController>();
                    break;
                }
            }
        }
    }

}
