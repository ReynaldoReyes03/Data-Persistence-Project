using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUIHandler : MonoBehaviour {
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private Transform panelsContainer;

    public void ClearLeaderboard() {
        foreach (Transform transform in panelsContainer.transform) Destroy(transform.gameObject);
    }

    public void UpdateLeaderboard() {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();

        if (bestScoresList != null) {
            for (int i = 0; i < bestScoresList.Count; i++) {
                GameObject panel = Instantiate(panelPrefab, panelsContainer);
                panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{i + 1} {bestScoresList[i]}";
            }
        }
    }
}
