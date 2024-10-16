﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.ultimate2d.combat
{
    public class GameManager : MonoBehaviour
    {
        private int[] values;
        private bool[] keys;

        public static KeyCode playerInput;

        public Text timerText;

        public ScoreManagerSO _scoreManager;

        private static GameManager _instance;
        public static GameManager Instance
        {
            get { return _instance; }
        }

        // audio
        AudioSource audioSource;
        Toggle toggle;

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

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = toggle.isOn;
            audioSource.Play();


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
            StartCoroutine("SpawnEnemies");


        }

        private IEnumerator SpawnEnemies()
        {
            // make placeholders for each one that fade from a glowing white
            var s1 = GameObject.Instantiate(emptyskelly, PlayerManager.Instance.transform.position + new Vector3(6, 0, 0), Quaternion.identity);
            var s2 = GameObject.Instantiate(emptyskelly, PlayerManager.Instance.transform.position + new Vector3(-6, 0, 0), Quaternion.identity);
            var s3 = GameObject.Instantiate(emptyskelly, PlayerManager.Instance.transform.position + new Vector3(-6, 2, 0), Quaternion.identity);
            var s4 = GameObject.Instantiate(emptyskelly, PlayerManager.Instance.transform.position + new Vector3(-6, -2, 0), Quaternion.identity);

            SpriteRenderer sr1 = s1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = s2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = s2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr4 = s2.GetComponent<SpriteRenderer>();
            sr1.color = Color.black;
            sr2.color = Color.black;
            sr3.color = Color.black;
            sr4.color = Color.black;
            var increment = new Color(0.01f, 0.01f, 0.01f);

            while(sr1.color != Color.white)
            {
                sr1.color += increment;
                if(sr1.color.b >= 1f)
                    sr1.color = Color.white;

                sr2.color = sr1.color;
                sr3.color = sr1.color;
                sr4.color = sr1.color;
                yield return null;
            }
            


            // spawning
            GameObject.Instantiate(skelly, s1.transform.position, Quaternion.identity);
            GameObject.Instantiate(skelly, s2.transform.position, Quaternion.identity);
            GameObject.Instantiate(skelly, s3.transform.position, Quaternion.identity);
            GameObject.Instantiate(skelly, s4.transform.position, Quaternion.identity);

            Destroy(s1);
            Destroy(s2);
            Destroy(s3);
            Destroy(s4);

        }

        public void EnemyDeathCount()
        {
            
            PlayerManager.Instance.killCount++;
            if(PlayerManager.Instance.killCount >= 4)
            {
                // save game data into scriptable object
                _scoreManager.time = timerText.text;

                // _scoreManager.damageTaken = 
                // _scoreManager.damageDealt = 

                // go to score screen
                SceneManager.LoadScene("ScoreScreen");
            }
        }
    }
}