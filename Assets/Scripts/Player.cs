using System;

[Serializable]
public class Player {
    public string name;
    public int score;

    public Player(string name, int score) {
        this.name = name;
        this.score = score;
    }

    public override string ToString() {
        return $"[{name}, {score}]";
    }
}
