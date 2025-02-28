using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ultimate2d.combat;

public class SpawnCheaterBoss : MonoBehaviour
{

    public GameObject bossPrefab;
    public Camera mainCamera;

    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.CompareTag("PlayerHurt"))
        {
            Debug.Log("spawning");
            // spawn cheater boss
            Vector3 spawnPoint = PlayerManager.Instance.transform.position + new Vector3(10f, 0, -5f);
            GameObject.Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
