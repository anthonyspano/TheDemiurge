using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreenManager : MonoBehaviour
{
    public InputField inputField;
    public GameObject inputFieldGO;
    public HighScoreManager highScoreManager;

    public void Start()
    {
        //highScoreManager.DisplayScores();

    }

    public void HideInputField()
    {
        try
        {
            foreach (HighScoreData.ScoreEntry entry in highScoreManager.highScoreData.highScores)
            {
                if(entry.playerName == "")
                {
                    Debug.Log("found the placeholder");
                    // change name to input name
                    entry.playerName = inputField.text;
                }
            }
        }
        catch(System.Exception e)
        {
            Debug.Log("name not found in list.");
        }

        inputFieldGO.SetActive(false);
        highScoreManager.DisplayScores();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
    }

}
