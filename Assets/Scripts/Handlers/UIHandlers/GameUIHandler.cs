using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private GameObject ball;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI playerBestScoreText;

    [Header("Containers")]
    [SerializeField] private GameObject gameOverContainer;
    [SerializeField] private GameObject winContainer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioClip victorySound;

    [Header("Scripts")]
    [SerializeField] private BestScoresManager bestScoresManager;

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
        audioSource.PlayOneShot(failSound);
        gameOverContainer.SetActive(isActive);
    }

    public void WinContainerVisibility(bool isActive) {
        audioSource.PlayOneShot(victorySound);
        winContainer.SetActive(isActive);

        Destroy(ball);
    }
}
