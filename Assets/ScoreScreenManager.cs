using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    public InputField inputField;
    public GameObject inputFieldGO;
    public HighScoreManager highScoreManager;

    public void Start()
    {
        highScoreManager.DisplayScores();
    }

    public void HideInputField()
    {
        highScoreManager.highScoreData.AddScore(inputField.text, 1);
        inputFieldGO.SetActive(false);
        highScoreManager.DisplayScores();
    }

}
