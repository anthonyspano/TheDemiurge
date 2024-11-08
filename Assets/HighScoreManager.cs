using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public HighScoreData highScoreData; // Reference to the Scriptable Object
    public Transform scoreListParent; // Parent transform for displaying scores
    public GameObject scoreEntryPrefab; // Prefab for each score entry UI element

    void Start()
    {
        highScoreData.LoadScores();
        DisplayScores();
    }

    public void DisplayScores()
    {
        foreach (Transform child in scoreListParent)
        {
            Destroy(child.gameObject); // Clear existing score entries
        }

        foreach (var scoreEntry in highScoreData.highScores)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, scoreListParent);
            Text[] texts = entry.GetComponentsInChildren<Text>();
            texts[0].text = scoreEntry.playerName;
            texts[1].text = scoreEntry.score.ToString();
            entry.SetActive(true);
        }
    }

    public void AddNewScore(string playerName, int score)
    {
        highScoreData.AddScore(playerName, score);
        highScoreData.SaveScores();
        DisplayScores();
    }
}

