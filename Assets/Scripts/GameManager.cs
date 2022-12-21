using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public bool correctName;

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

    public SettingsData GetSettigns() {
        SettingsData settings = null;
        string saveFilePath = Path.Combine(Application.persistentDataPath + "/Settings.json");

        if (File.Exists(saveFilePath)) {
            string json = File.ReadAllText(saveFilePath);
            settings = JsonUtility.FromJson<SettingsData>(json);
        }

        return settings;
    }

    public void SaveSettings(SettingsMenuUIHandler settings) {
        SettingsData data = new SettingsData(settings);

        string saveFilePath = Path.Combine(Application.persistentDataPath + "/Settings.json");
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }
}
