using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [HideInInspector] public string playerName;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<Player> GetBestScores() {
        List<Player> bestScores = null;
        string saveFilePath = Application.persistentDataPath + "/BestScores.json";

        if (File.Exists(saveFilePath)) {
            string json = File.ReadAllText(saveFilePath);
            bestScores = JsonUtility.FromJson<BestScores>(json).players;
        }

        return bestScores;
    }

    public void SaveBestScoresList(List<Player> bestScoresList) {
        BestScores data = new BestScores();
        data.players = bestScoresList;

        string saveFilePath = Application.persistentDataPath + "/BestScores.json";
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }
}
