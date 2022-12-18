using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BestScoresManager : MonoBehaviour {

    public bool IsTheNewBestScore(int score) {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();
        bool isNewBestScore = false;

        if (GameManager.Instance== null || score > bestScoresList[0].score) {
            isNewBestScore = true;
        }

        return isNewBestScore;
    }

    public bool CheckScore(int score) {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();
        bool canBeAddedToList = false;

        if (bestScoresList.Count < 10 || score > bestScoresList[bestScoresList.Count - 1].score) {
            canBeAddedToList = true;
        }

        return canBeAddedToList;
    }

    public void AddToList(Player player) {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();

        bestScoresList.Add(player);

        bestScoresList = (from bestScore in bestScoresList
                          orderby bestScore.score descending
                          select bestScore).ToList();

        while (bestScoresList.Count > 10) {
            bestScoresList.RemoveAt(bestScoresList.Count - 1);
        }

        GameManager.Instance.SaveBestScoresList(bestScoresList);
    }

    public Player GetBestScore() {
        List<Player> bestScoresList = GameManager.Instance.GetBestScores();
        Player bestScore = null;

        if (bestScoresList != null) {
            bestScore = bestScoresList[0];
        }

        return bestScore;
    }
}
