using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour {
    [Header("Prefabs")]
    [SerializeField] private GameObject leaderboardElementPrefab;

    [Header("Containers")]
    [SerializeField] private Transform panelsContainer;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("Buttons")]
    [SerializeField] private Button resetButton;

    public void ClearLeaderboard() {
        text.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
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
            resetButton.gameObject.SetActive(false);
        }
    }

    public void ResetBestScoresList() {
        GameManager.Instance.ResetBestScoresList();
    }
}
