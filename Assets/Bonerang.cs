using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonerang : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((enemyLayer.value & (1 << col.gameObject.layer)) > 0)
            Destroy(gameObject);
    }
}
