using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUIHandler : MonoBehaviour {
    [SerializeField] private GameObject leaderboardElementPrefab;
    [SerializeField] private Transform panelsContainer;
    [SerializeField] private TextMeshProUGUI text;

    public void ClearLeaderboard() {
        text.gameObject.SetActive(false);
        foreach (Transform transform in panelsContainer.transform) Destroy(transform.gameObject);
    }

    public void UpdateLeaderboard() {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();

        if (bestScoresList != null) {
            for (int i = 0; i < bestScoresList.Count; i++) {
                Player player = bestScoresList[i];
                GameObject leaderboardElement = Instantiate(leaderboardElementPrefab, panelsContainer);

                LeaderboardElement element = leaderboardElement.GetComponent<LeaderboardElement>();
                element.numberText.text = (i + 1).ToString();
                element.playerNameText.text = player.name;
                element.scoreText.text = player.score.ToString();
            }
        } else {
            text.gameObject.SetActive(true);
        }
    }
}
