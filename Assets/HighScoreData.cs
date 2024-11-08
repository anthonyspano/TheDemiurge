using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "GameData/HighScoreData", order = 1)]
public class HighScoreData : ScriptableObject
{
    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;
    }

    public List<ScoreEntry> highScores = new List<ScoreEntry>();

    public void AddScore(string playerName, int score)
    {
        highScores.Add(new ScoreEntry { playerName = playerName, score = score });
        SortScores();
    }

    private void SortScores()
    {
        highScores.Sort((x, y) => y.score.CompareTo(x.score)); // Sort descending by score
        if (highScores.Count > 10) // Limit to top 10 scores
        {
            highScores.RemoveRange(10, highScores.Count - 10);
        }
    }

    // saving and loading scores
    public void SaveScores()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    



}
