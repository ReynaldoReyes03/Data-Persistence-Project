using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public void OpenGameScene() {
        if (Time.timeScale == 0.0f) Time.timeScale = 1.0f;
        if (GameManager.Instance.correctName) SceneManager.LoadScene(1);
    }

    public void OpenMenuScene() {
        if (Time.timeScale == 0.0f) Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
