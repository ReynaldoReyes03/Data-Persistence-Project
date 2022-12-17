using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BestScoresClass : MonoBehaviour {
    public List<Player> bestScores = new List<Player>();
    public string saveFilePath;

    private void Start() {
        saveFilePath = Application.persistentDataPath + "/BestScores.json";

        Player john = new Player("Jhon", 70);
        Player sam = new Player("Sam", 10);
        Player roger = new Player("Roger", 20);
        Player a = new Player("a", 40);
        Player b = new Player("b", 150);
        Player c = new Player("c", 2);
        Player d = new Player("d", 0);
        Player e = new Player("e", 1);
        Player f = new Player("f", 2);
        Player g = new Player("g", 11);
        Player h = new Player("h", 280);
        Player ii = new Player("ii", 50);
        Player j = new Player("j", 20);
        Player k = new Player("k", 30);
        Player l = new Player("l", 0);

        bestScores.Add(john);
        bestScores.Add(sam);
        bestScores.Add(roger);
        bestScores.Add(a);
        bestScores.Add(b);
        bestScores.Add(c);
        bestScores.Add(d);
        bestScores.Add(e);
        bestScores.Add(f);
        bestScores.Add(g);
        bestScores.Add(h);
        bestScores.Add(ii);
        bestScores.Add(j);
        bestScores.Add(k);
        bestScores.Add(l);

        bestScores = (from p in bestScores
                      orderby p.score descending
                      select p).ToList();

        while (bestScores.Count > 10) {
            bestScores.RemoveAt(bestScores.Count - 1);
        }
    }

    [Serializable]
    class BestScores {
        public List<Player> players;
    }

    public void SaveColor() {
        BestScores data = new BestScores();
        data.players = bestScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
        if (File.Exists(saveFilePath)) print("save");
    }

    public void LoadColor() {
        if (File.Exists(saveFilePath)) {
            string json = File.ReadAllText(saveFilePath);
            BestScores data = JsonUtility.FromJson<BestScores>(json);

            foreach (Player player in data.players) print(player);
        }
    }

    public void add() {
        Player pepe = new Player("pepe", 11);
        print(bestScores[bestScores.Count - 1]);
        if (pepe.score > bestScores[bestScores.Count - 1].score) {
            print("Entras");

            if (pepe.score > bestScores[0].score) {
                print(pepe);
            }
        }
    }
}
