using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int[] values;
    private bool[] keys;

    public static KeyCode playerInput;

    public Text timerText;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        // values = (int[])System.Enum.GetValues(typeof(KeyCode));
        // keys = new bool[values.Length];

        timerText.text = "0";

		// singleton
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

    }

    void Start()
    {
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // for(int i=0; i<values.Length; i++)
        // {
        //     keys[i] = Input.GetKey((KeyCode)values[i]);
            
        //     if(keys[i])
        //     {
        //         playerInput = (KeyCode)values[i];
        //         //Debug.Log((int)playerInput);
        //     }
        // }  

        timerText.text = Time.timeSinceLevelLoad.ToString();      
    }

    public void GameStart()
    {
        SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
    }

    public GameObject skelly;
    public void BeginLevel()
    {
        // keep json file of enemies and positions they need to spawn
        
        // spawn two skellies to left and right of player for now
        Debug.Log("spawning");
        GameObject.Instantiate(skelly, PlayerManager.Instance.transform.position + new Vector3(6, 0, 0), Quaternion.identity);
        Debug.Log("spawning");
        GameObject.Instantiate(skelly, PlayerManager.Instance.transform.position + new Vector3(-6, 0, 0), Quaternion.identity);

    }
}
