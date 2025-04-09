using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// exists for a short timer, then spawns the intended enemy on top of it
// then deletes itself before the enemy moves
public class EnemyPlaceholder : MonoBehaviour
{
    public GameObject realEnemyPrefab;
    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SpawnRealEnemy());
        StartCoroutine(SpriteFadeInEffect());
    }

    IEnumerator SpawnRealEnemy()
    {
        // delay
        yield return new WaitForSeconds(1.5f);

        // spawn real enemy on top of this
        Instantiate(realEnemyPrefab, transform.position, Quaternion.identity);
        yield return null;

        // kill this
        Destroy(gameObject);

    }

    IEnumerator SpriteFadeInEffect() 
    {
        //Color noColor = new Color(0,0,0,0);
        while(spriteRenderer.color != Color.white)
        {
            spriteRenderer.color += new Color(0.05f,0.05f,0.05f,0);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
