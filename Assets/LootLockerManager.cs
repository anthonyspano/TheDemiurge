using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });

        LootLockerSDKManager.SetPlayerName("Some other name", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            } else
            {
                Debug.Log("Error setting player name");
            }
        });

        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player name: " + response.name);
            } else
            {
                Debug.Log("Error getting player name");
            }
        });


        // leaderboards

        string leaderboardKey = "my_leaderboard";
        int score = 1000;

        LootLockerSDKManager.SubmitScore("", score, leaderboardKey, (response) =>
        {
            if (!response.success) {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                return;
            } 
            Debug.Log("Successfully submitted score!");
        
        });

        int count = 50;

        LootLockerSDKManager.GetScoreList(leaderboardKey, count, 0, (response) =>
        {
            if (!response.success) {
                Debug.Log("Could not get score list!");
                Debug.Log(response.errorData.ToString());
                return;
            } 
            Debug.Log("Successfully got score list!");
        });





    }













}