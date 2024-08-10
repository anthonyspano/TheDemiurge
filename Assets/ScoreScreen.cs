using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public ScoreManagerSO _scoreManager;
    public GameObject completedTimeObject;
    public Text completedTime;
    public Text completeWaves;

    private void Awake()
    {
        completedTime.text = _scoreManager.time;
        completeWaves.text = _scoreManager.wavesCompleted.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
