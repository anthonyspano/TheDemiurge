using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LootLockerManager : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(StartGuestSession());
    }

    IEnumerator StartGuestSession()
    {
        bool done = false;
        // Initialize the LootLocker SDK with your game settings
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                Debug.LogError($"Error starting LootLocker session: {response.statusCode}");

            }
            else
            {
                Debug.Log("successfully started LootLocker session");
                done = true;
            }

            
            
        });

        yield return new WaitUntil(() => done); 
        
        StartCoroutine(GetPlayerName());

    }

    IEnumerator SetPlayerName()
    {
        bool done = false;
        LootLockerSDKManager.SetPlayerName("test name", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            } else
            {
                Debug.LogError($"Error starting LootLocker session: {response.errorData.ToString()}");
            }
            done = true;
        });

        yield return new WaitUntil(() => done);

        GetPlayerName();

    }

    IEnumerator GetPlayerName()
    {
        bool done = false;
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player name: " + response.name);
            } else
            {
                Debug.Log("Error getting player name");
            }
            done = true;
        });

        yield return new WaitUntil(() => done);

        StartCoroutine(SubmitScore());

    }

    IEnumerator SubmitScore()
    {
        bool done = false;
        // leaderboards

        string leaderboardKey = "savior";
        int score = 10;

        LootLockerSDKManager.SubmitScore("", score, leaderboardKey, (response) =>
        {
            if (!response.success) {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                return;
            } 
            else {
                done = true;
                Debug.Log("Successfully submitted score!");
            }
            
        
        });

        yield return new WaitUntil(() => done);

        StartCoroutine(GetScoreList());
    }

    IEnumerator GetScoreList()
    {
        bool done = false;
        int count = 10;
        string leaderboardKey = "savior";

        Debug.Log("Getting score list...");
        LootLockerSDKManager.GetScoreList(leaderboardKey, count, (response) =>
        {
            if (!response.success) {
                Debug.Log("Could not get score list!");
                Debug.Log(response.errorData.ToString());
                return;
            } 
            Debug.Log("Successfully got score list!");
            foreach(var entries in response.items)
            {
                Debug.Log($"Player {entries.player.name} had a score of {entries.score} ranking them {entries.rank} on the Leaderboard");
            }

        });

        yield return new WaitUntil(() => done);

        
    }





    













}