using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuUIHandler : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;

    [Header("References")]
    [SerializeField] private MainManager mainManager;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button cancelButton;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainManager.m_GameOver) {
            if (!settingsPanel.activeSelf) {
                if (Time.timeScale == 1.0f) {
                    pauseButton.onClick.Invoke();
                } else {
                    resumeButton.onClick.Invoke();
                }
            } else {
                cancelButton.onClick.Invoke();
            }
        }
    }

    public void PauseGame() {
        if (Time.timeScale == 1.0f) {
            Time.timeScale = 0.0f;
        } else {
            Time.timeScale = 1.0f;
        }
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
