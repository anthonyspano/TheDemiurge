using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ultimate2d.combat
{

public class HighScoreBook : MonoBehaviour
{
    public HighScoreManager highScoreManager;
    public InputField inputField;

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

    void Update()
    {
        if(PlayerInput.LightAttack())
        {
            if(inputField.text != "")
            {
                GameManager.Instance.StartBeginLevelCoroutine();
            }

        }
    }
}
}