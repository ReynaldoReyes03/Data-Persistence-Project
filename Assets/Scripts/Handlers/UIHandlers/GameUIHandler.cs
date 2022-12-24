using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour {
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI playerBestScoreText;
    [SerializeField] private GameObject gameOverContainer;
    [SerializeField] private GameObject winContainer;

    public BestScoresManager bestScoresManager;

    private void Start() {
        Player bestScore = bestScoresManager.GetBestScore();

        if (bestScore != null) {
            playerBestScoreText.text = $"{bestScore.name}: {bestScore.score}";
        } else {
            playerBestScoreText.text = $"0";
        }

        UpdateScoreText(0);
    }

    public void UpdateScoreText(int score) {
        currentScoreText.text = $"{GameManager.Instance.playerName}: {score}";
    }

    public void UpdateBestScoreText(int score) {
        playerBestScoreText.text = $"{GameManager.Instance.playerName}: {score}";
    }

    public void GameOverContainerVisibility(bool isActive) {
        gameOverContainer.SetActive(isActive);
    }

    public void WinContainerVisibility(bool isActive) {
        winContainer.SetActive(isActive);
        Time.timeScale = 0.0f;
    }
}
