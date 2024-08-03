using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;

    SetLeaderboardItemInfo[] setLeaderboardItemInfo;

    
    public void Awake() {
        UpdateLeaderBoard();
    }

    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        for (int i =0; i < lapCounters.Count; i++)
        {
            setLeaderboardItemInfo[i].SetDriverNameText(lapCounters[i].gameObject.name);
        }
    }

    public void UpdateLeaderBoard() {

        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        setLeaderboardItemInfo = new SetLeaderboardItemInfo[carLapCounterArray.Length];

        for (int i = 0; i < carLapCounterArray.Length; i++ )
        {
            GameObject leaderboardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);

            setLeaderboardItemInfo[i] = leaderboardInfoGameObject.GetComponent<SetLeaderboardItemInfo>();

            setLeaderboardItemInfo[i].SetPositionText($"{i + 1}.");
        }
    }
}
