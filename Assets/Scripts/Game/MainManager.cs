using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {
    private int lineCount = 6;
    private int m_Points;
    private int totalPoints;

    private bool m_Started = false;
    [HideInInspector] public bool m_GameOver = false;

    [Header("Components")]
    [SerializeField] private Rigidbody ball;
    [SerializeField] private GameUIHandler gameUIHandler;
    [SerializeField] private BestScoresManager bestScoresManager;

    [Header("Bricks")]
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private Brick brickPrefab;

    // Start is called before the first frame update
    private void Start() {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};

        for (int i = 0; i < lineCount; ++i) {
            for (int x = 0; x < perLine; ++x) {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                var brick = Instantiate(brickPrefab, position, Quaternion.identity, bricksContainer);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);

                totalPoints += pointCountArray[i];
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
            if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1.0f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void AddPoint(int point) {
        m_Points += point;
        gameUIHandler.UpdateScoreText(m_Points);

        if (bestScoresManager.IsTheNewBestScore(m_Points)) {
            gameUIHandler.UpdateBestScoreText(m_Points);
        }

        if (m_Points == totalPoints) {
            StartCoroutine(ActiveWinContainer());
            CheckScore();
        }
    }

    public void GameOver() {
        m_GameOver = true;

        StartCoroutine(ActiveGameOverContainer());
        CheckScore();
    }

    private void CheckScore() {
        if (bestScoresManager.CheckScore(m_Points)) {
            Player player = new Player(GameManager.Instance.playerName, m_Points);

            bestScoresManager.AddToList(player);

            if (bestScoresManager.IsTheNewBestScore(m_Points)) {
                gameUIHandler.UpdateBestScoreText(m_Points);
            }
        }
    }

    IEnumerator ActiveWinContainer() {
        yield return new WaitForSeconds(0.3f);
        gameUIHandler.WinContainerVisibility(true);
    }

    IEnumerator ActiveGameOverContainer() {
        yield return new WaitForSeconds(0.3f);
        gameUIHandler.GameOverContainerVisibility(true);
    }
}
