using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public void OpenGameScene() {
        if (GameManager.Instance.correctName) SceneManager.LoadScene(1);
    }

    public void OpenMenuScene() {
        SceneManager.LoadScene(0);
    }
}
