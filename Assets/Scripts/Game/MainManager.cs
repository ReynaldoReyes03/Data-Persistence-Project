using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody ball;

    public GameUIHandler gameUIHandler;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public BestScoresManager bestScoresManager;

    // Start is called before the first frame update
    void Start() {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};

        for (int i = 0; i < lineCount; ++i) {
            for (int x = 0; x < perLine; ++x) {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update() {
        if (!m_Started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDirection = new Vector3(randomDirection, 1, 0);
                forceDirection.Normalize();

                ball.transform.SetParent(null);
                ball.AddForce(forceDirection * 2.0f, ForceMode.VelocityChange);
            }
        } else if (m_GameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point) {
        m_Points += point;
        gameUIHandler.UpdateScoreText(m_Points);

        if (bestScoresManager.IsTheNewBestScore(m_Points)) {
            gameUIHandler.UpdateBestScoreText(m_Points);
        }
    }

    public void GameOver() {
        m_GameOver = true;
        gameUIHandler.GameOverTextVisibility(true);

        if (bestScoresManager.CheckScore(m_Points)) {
            Player player = new Player(GameManager.Instance.playerName, m_Points);

            bestScoresManager.AddToList(player);

            if (bestScoresManager.IsTheNewBestScore(m_Points)) {
                gameUIHandler.UpdateBestScoreText(m_Points);
            }
        }
    }
}
