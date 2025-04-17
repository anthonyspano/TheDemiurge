using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

// there are 12 spawn positions
// highest score is 240

namespace com.ultimate2d.combat
{
    public class GameManager : MonoBehaviour
    {
        // private int[] values;
        // private bool[] keys;

        public static KeyCode playerInput;

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
        public int maxWaves;

        // audio
        AudioSource audioSource;

        // UI
        public Text timerText;
        private bool timerEnabled;
        public Text waveInfo;
        public Text scoreText;
        

        // score
        public string currentPlayerScore;
        public HighScoreData highScoreData;
        public GameObject highScoreBook;
        public InputField inputField;


        void Awake()
        {
            // values = (int[])System.Enum.GetValues(typeof(KeyCode));
            // keys = new bool[values.Length];

            //timerText.text = "0";
            //timerEnabled = true;

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
            // make spawn positions mid distance between player and center of arena
            // center of arena
            Vector3 arenaCenter = new Vector3(3, 66, 0);
            spawnPositions.Add(new Vector3(3, 0, 0) + arenaCenter);
            spawnPositions.Add(new Vector3(-3, 0, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, -3, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, 3, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(4, 0, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(-4, 0, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, -4, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, 4, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(2, 0, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(-2, 0, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, -2, 0)+ arenaCenter);
            spawnPositions.Add(new Vector3(0, 2, 0)+ arenaCenter);

            
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
            //         Debug.Log(playerInput);
            //         Debug.Log((int)playerInput);
            //     }
            // }  

            if(timerEnabled)
                timerText.text = Time.timeSinceLevelLoad.ToString(); 

            scoreText.text = (PlayerManager.Instance.killCount * 10).ToString();
            

                 
        }

        public void GameStart()
        {
            SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
        }

        public GameObject skellyPlaceholderPrefab;
        public GameObject meleePlaceHolderPrefab;

        public Text bookDialogue;
        private string levelDialogue = "I wonder if they still think about me...";

        public InputField bidInputField;

        public void StartBeginLevel()
        {
            // bring up leaderboard book object (book object brings up scores and input field, input field starts the level)
            highScoreBook.SetActive(true);
            if(ScoreRecorder.Instance.previousPlayerBid != 0)
                highScoreBook.transform.GetChild(2).GetComponent<InputField>().text = ScoreRecorder.Instance.previousPlayerBid.ToString();
            else
            {
                bidInputField.Select();
            }

//            StartCoroutine(SetInputField());
            
        }

        IEnumerator SetInputField()
        {
            yield return new WaitForSeconds(0.2f);
            if(ScoreRecorder.Instance.previousPlayerBid != 0)
                highScoreBook.transform.GetChild(2).GetComponent<InputField>().text = ScoreRecorder.Instance.previousPlayerBid.ToString();
        }


        public void StartBeginLevelCoroutine()
        {
            // save string entry to be put into high score data
            currentPlayerScore = inputField.text;

            ScoreRecorder.Instance.previousPlayerBid = int.Parse(currentPlayerScore);

            // disable book
            highScoreBook.SetActive(false);

            StartCoroutine(BeginLevel());
        }

        // activated by in-game book object
        public IEnumerator BeginLevel()
        {
            // show book dialogue
            // for(int i = 0; i < levelDialogue.Length; i++)
            // {
            int i = 0;
            string temp = "";
            // while(bookDialogue.text.Length < levelDialogue.Length)
            // {
            //     bookDialogue.text += levelDialogue[i];
            //     i+=1;
            //     yield return new WaitForSeconds(0.08f);
            
            // }

            // TBI: keep json file of enemies and positions they need to spawn

            
            // spawn two skellies to left and right of player for now
            StartCoroutine(SpawnEnemies((int.Parse(currentPlayerScore) / 10)));

            yield return new WaitForSeconds(1f);
            bookDialogue.text = "";
        }

        private IEnumerator ToggleWavePrompt()
        {
            waveInfo.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(2.5f);

            waveInfo.gameObject.SetActive(false);



        }

        private IEnumerator SpawnEnemies(int enemiesToSpawn)
        {
            System.Random rand = new System.Random();
            int index;
            //Debug.Log(enemiesToSpawn);
            
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                
                index = i;
                
                if(index >= spawnPositions.Count)
                    index %= spawnPositions.Count;

                try
                {
                    // randomly pick between skelly and charger

                    int enemyChoice = rand.Next(1,3);
                    //int enemyChoice = 2;
                    
                    switch(enemyChoice)
                    {
                        case 1:
                            // tbi: spawn silhouette objects that spawn actual enemies in their place
                            // spawn skelly at fixed pos
                            GameObject.Instantiate(skellyPlaceholderPrefab, spawnPositions[index], Quaternion.identity);
                            break;
                        case 2:
                            // spawn melee enemy
                            GameObject.Instantiate(meleePlaceHolderPrefab, spawnPositions[index], Quaternion.identity);
                            break;
                        default:
                            Debug.Log("Random function out of range");
                            break;
                    }
                    
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

                // interval between spawns
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
            
            yield return new WaitUntil(() => AllEnemiesAreDead());

            Debug.Log("adding blank score entry");
            highScoreData.AddScore("", int.Parse(currentPlayerScore));
            highScoreData.SaveScores();

            yield return new WaitForSeconds(2.5f);
            
            SceneManager.LoadScene("ScoreScreen");

        }

        private float timeInCurrentWave;
        public float waveTimeLimit = 20;

        private IEnumerator EnemyWaveManager()
        {
            for(int i=0; i<maxWaves; i++)
            {
                waveInfo.text = "Wave " + (i+1).ToString() + "/" + maxWaves.ToString();
                StartCoroutine(ToggleWavePrompt());
                timeInCurrentWave = 0;
                Debug.Log("spawning more enemies");
                StartCoroutine(SpawnEnemies(numberOfEnemies));
                
                yield return new WaitUntil(() => AllEnemiesAreDead()); // || OutOfTime());
                numberOfEnemies += 3;
            }
            Debug.Log("ending game");

            StartCoroutine(EndWave());
            
        }

        private bool AllEnemiesAreDead()
        {
            
            var enemiesAliveCurrently = GameObject.FindGameObjectsWithTag("Enemy");
            //Debug.Log(enemiesAliveCurrently.Length);
            if(enemiesAliveCurrently.Length > 0)
                return false;

            return true;
            
        }

        private bool OutOfTime()
        {
            // returns true if timer has exceeded wave limit
            timeInCurrentWave += Time.deltaTime;
            if(timeInCurrentWave > waveTimeLimit)
            {
                Debug.Log("out of time");
                return true;
            }

            return false;
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

        private IEnumerator EndWave()
        {
            timerEnabled = false;

            // kill rest of enemies
            //GameObject[] enemiesAliveCurrently = new GameObject[20];
            var enemiesAliveCurrently = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i=0; i<enemiesAliveCurrently.Length; i++)
            {
                try
                {
                    //enemiesAliveCurrently[i].GetComponent<EnemyTakeDamage>().healthSystem.Damage(1000000);
                    enemiesAliveCurrently[i].GetComponent<Animator>().SetBool("BlowUp", true);
                    // Debug.Log(enemiesAliveCurrently[i].transform.name);
                    // Debug.Log(enemiesAliveCurrently[i].GetComponent<EnemyTakeDamage>());
                }
                catch(Exception e)
                { 
                    //Debug.Log("no more enemies left");
                    //Debug.Log(enemiesAliveCurrently[i].transform.name);
                }


            }
            
            // set score to be added to scriptable object
            Debug.Log("adding blank score entry");
            highScoreData.SaveScores();

            yield return new WaitForSeconds(2.5f);
            
            SceneManager.LoadScene("ScoreScreen");

            
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}