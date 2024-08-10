using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreManagerSO", order = 1)]
public class ScoreManagerSO : ScriptableObject
{
    public string time;
    public int wavesCompleted;
    public float damageTaken;
    public float damageDealt;


}
