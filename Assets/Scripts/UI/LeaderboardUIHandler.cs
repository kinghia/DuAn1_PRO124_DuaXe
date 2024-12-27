using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    public GameObject leaderboardItemPrefab; // Prefab của một mục leaderboard
    private List<SetLeaderboardItemInfo> setLeaderboardItemInfoList = new List<SetLeaderboardItemInfo>();

    public void Awake()
    {
        UpdateLeaderBoard();
    }

    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        // Đảm bảo số lượng mục khớp với danh sách lapCounters
        if (lapCounters.Count != setLeaderboardItemInfoList.Count)
        {
            Debug.LogWarning("Lap counter list size does not match leaderboard size. Updating leaderboard...");
            UpdateLeaderBoard();
        }

        // Gán tên driver cho từng mục
        for (int i = 0; i < lapCounters.Count; i++)
        {
            if (setLeaderboardItemInfoList[i] != null)
            {
                setLeaderboardItemInfoList[i].SetDriverNameText(lapCounters[i].gameObject.name);
            }
        }
    }

    public void UpdateLeaderBoard()
    {
        // Lấy VerticalLayoutGroup chứa các mục leaderboard
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        // Dọn dẹp danh sách cũ
        foreach (Transform child in leaderboardLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        setLeaderboardItemInfoList.Clear();

        // Tìm tất cả CarLapCounter trên màn chơi
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        // Tạo các mục leaderboard mới
        for (int i = 0; i < carLapCounterArray.Length; i++)
        {
            GameObject leaderboardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);

            // Kiểm tra và thêm thành phần SetLeaderboardItemInfo
            SetLeaderboardItemInfo itemInfo = leaderboardInfoGameObject.GetComponent<SetLeaderboardItemInfo>();
            if (itemInfo != null)
            {
                itemInfo.SetPositionText($"{i + 1}.");
                setLeaderboardItemInfoList.Add(itemInfo);
            }
            else
            {
                Debug.LogError("Leaderboard item prefab is missing SetLeaderboardItemInfo component!");
                Destroy(leaderboardInfoGameObject); // Hủy nếu không hợp lệ
            }
        }
    }
}
