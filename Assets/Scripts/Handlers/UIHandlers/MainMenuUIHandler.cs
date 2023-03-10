using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject disabledPlayButton;
    [SerializeField] private Button playButton;

    public void CheckName() {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrEmpty(playerName)) {
            playButton.gameObject.SetActive(false);
            disabledPlayButton.SetActive(true);

            GameManager.Instance.correctName = false;
        } else {
            disabledPlayButton.SetActive(false);
            playButton.gameObject.SetActive(true);

            GameManager.Instance.correctName = true;
        }
    }

    public void ClearInputField() {
        nameInputField.text = string.Empty;
    }

    public void OpenGitHubURL() {
        Application.OpenURL(GameManager.Instance.gitHubURL);
    }

    public void OpenProjectURL() {
        Application.OpenURL(GameManager.Instance.projectRepositoryURL);
    }

    public void SavePlayerName() {
        GameManager.Instance.playerName = nameInputField.text.Trim();
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            Application.OpenURL(GameManager.Instance.gitHubURL);
        #else
            Application.Quit();
        #endif
    }
}
