using UnityEngine;
using UnityEngine.UI;

public class HotkeysHandler : MonoBehaviour {
    [Header("Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject leaderboardPanel;

    [Header("Title Panel Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    [Header("Start Game Panel Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button cancelButton;

    [Header("Settings Panel Buttons")]
    [SerializeField] private Button saveSettingButton;
    [SerializeField] private Button discardSettingsButton;

    [Header("Leaderboard Panel Buttons")]
    [SerializeField] private Button closeLeaderboardButton;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (titlePanel.activeSelf) {
                startButton.onClick.Invoke();
            } else if (startGamePanel.activeSelf) {
                playButton.onClick.Invoke();
            } else if (settingsPanel.activeSelf) {
                saveSettingButton.onClick.Invoke();
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (titlePanel.activeSelf) {
                exitButton.onClick.Invoke();
            } else if (startGamePanel.activeSelf) {
                cancelButton.onClick.Invoke();
            } else if (settingsPanel.activeSelf) {
                discardSettingsButton.onClick.Invoke();
            } else if (leaderboardPanel.activeSelf) {
                closeLeaderboardButton.onClick.Invoke();
            }
        }
    }
}
