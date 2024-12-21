using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreBook : MonoBehaviour
{
    public HighScoreManager highScoreManager;

    void OnEnabled()
    {
        // bring up scores and input field
        highScoreManager.highScoreData.LoadScores();
        
        StartCoroutine(WaitToShowScores());
        

        

       

    }

    IEnumerator WaitToShowScores()
    {
        yield return null;
        //yield return new WaitForSeconds(5);
        highScoreManager.DisplayScores();
    }
}
