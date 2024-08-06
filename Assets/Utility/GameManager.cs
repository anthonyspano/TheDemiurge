using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace com.ultimate2d.combat
{
    public class GameManager : MonoBehaviour
    {
        private int[] values;
        private bool[] keys;

        public static KeyCode playerInput;

        public Text timerText;
        private bool timerEnabled;

        public ScoreManagerSO _scoreManager;

        private static GameManager _instance;
        public static GameManager Instance
        {
            get { return _instance; }
        }

        // enemy spawner
        public List<Vector3> spawnPositions;
        public int enemiesToSpawn = 4;
        private enum CurrentWave {First, Second, Third};
        private CurrentWave currentWave;
        int numberOfEnemies = 4;
        private int maxWaves = 4;

        // audio
        AudioSource audioSource;

        void Awake()
        {
            // values = (int[])System.Enum.GetValues(typeof(KeyCode));
            // keys = new bool[values.Length];

            timerText.text = "0";
            timerEnabled = true;

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

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.Play();

            // populate Spawn Positions list
            spawnPositions = new List<Vector3>();
            spawnPositions.Add(new Vector3(3, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(-3, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, -3, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, 3, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(4, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(-4, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, -4, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, 4, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(2, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(-2, 0, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, -2, 0) + PlayerManager.Instance.transform.position);
            spawnPositions.Add(new Vector3(0, 2, 0) + PlayerManager.Instance.transform.position);


        }

        void Update()
        {

            if(Input.GetKeyDown(KeyCode.Escape))
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

            if(timerEnabled)
                timerText.text = Time.timeSinceLevelLoad.ToString();      
        }

        public void GameStart()
        {
            SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
        }

        public GameObject skelly;
        public GameObject emptyskelly;
        public void BeginLevel()
        {
            // keep json file of enemies and positions they need to spawn

            
            // spawn two skellies to left and right of player for now
            StartCoroutine(EnemyWaveManager());


        }

        private IEnumerator SpawnEnemies(int enemiesToSpawn)
        {
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                try
                {
                    GameObject.Instantiate(skelly, spawnPositions[i], Quaternion.identity);
                }
                catch (Exception e)
                {
                    i = enemiesToSpawn - spawnPositions.Count;
                }

                yield return new WaitForSeconds(0.3f);

            }

            yield return null;
            
            // make placeholders for each one that fade from a glowing white
            //sr1.color = Color.black;
            // var increment = new Color(0.01f, 0.01f, 0.01f);

            // while(sr1.color != Color.white)
            // {
            //     sr1.color += increment;
            //     if(sr1.color.b >= 1f)
            //         sr1.color = Color.white;

            //     yield return null;
            // }
            

        }

        private IEnumerator EnemyWaveManager()
        {
            for(int i=0; i<maxWaves; i++)
            {
                Debug.Log("spawning more enemies");
                StartCoroutine(SpawnEnemies(numberOfEnemies));
                yield return new WaitForSeconds(3f);
                numberOfEnemies += 4;
            }
            Debug.Log("ending game");

            StartCoroutine(EndGame());
            
        }

        public void EnemyDeathCount()
        {
        //     // spawn enemies based on time           
             PlayerManager.Instance.killCount++;
        //     Debug.Log(PlayerManager.Instance.killCount);
        //     if(PlayerManager.Instance.killCount >= 20)
        //     {
        //         // save game data into scriptable object?
        //         _scoreManager.time = timerText.text;

        //         // stop game timer
        //         timerEnabled = false;



        //         // _scoreManager.damageTaken = 
        //         // _scoreManager.damageDealt = 

        //         // go to score screen after x seconds
        //         StartCoroutine("EndGame");
                
        //     }
        //     else if(PlayerManager.Instance.killCount >= 3)
        //     {
        //         enemiesToSpawn++; 
        //         if(enemiesToSpawn > 4) enemiesToSpawn = 4;
        //         StartCoroutine("SpawnEnemies");
        //     }
        //     else
        //         enemiesToSpawn = 2;
        }

        private IEnumerator EndGame()
        {
            timerEnabled = false;
            _scoreManager.time = timerText.text;

            // kill rest of enemies
            GameObject[] enemiesAliveCurrently = new GameObject[20];
            enemiesAliveCurrently = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i=0; i<enemiesAliveCurrently.Length; i++)
            {
                try
                {
                    enemiesAliveCurrently[i].GetComponent<EnemyTakeDamage>().healthSystem.Damage(1000000);
                    // Debug.Log(enemiesAliveCurrently[i].transform.name);
                    // Debug.Log(enemiesAliveCurrently[i].GetComponent<EnemyTakeDamage>());
                }
                catch(Exception e)
                { 
                    //Debug.Log("no more enemies left");
                    Debug.Log(enemiesAliveCurrently[i].transform.name);
                }


            }
            

            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("ScoreScreen");
        }
    }
}