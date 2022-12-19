using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour {
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public BestScoresManager bestScoresManager;

    private void Start() {
        Player bestScore = bestScoresManager.GetBestScore();

        if (bestScore != null) {
            bestScoreText.text = $"Best Score: {bestScore.name} : {bestScore.score}";
        } else {
            bestScoreText.text = $"Best Score: 0";
        }

        UpdateScoreText(0);
    }

    public void UpdateScoreText(int score) {
        currentScoreText.text = $"SCORE: {GameManager.Instance.playerName} : {score}";
    }

    public void UpdateBestScoreText(int score) {
        bestScoreText.text = $"Best Score: {GameManager.Instance.playerName} : {score}";
    }

    public void GameOverTextVisibility(bool isActive) {
        gameOverText.gameObject.SetActive(isActive);
    }
}
