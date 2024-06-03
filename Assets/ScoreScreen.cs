using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public ScoreManagerSO _scoreManager;
    public GameObject completedTimeObject;
    public Text completedTime;

    private void Awake()
    {
        completedTime.text = _scoreManager.time;
    }

}
