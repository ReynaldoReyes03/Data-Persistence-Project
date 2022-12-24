using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuUIHandler : MonoBehaviour {
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private MainManager mainManager;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainManager.m_GameOver) {
            if (Time.timeScale == 1.0f) {
                pauseButton.onClick.Invoke();
            } else {
                resumeButton.onClick.Invoke();
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
            Application.OpenURL(GameManager.Instance.webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }
}
