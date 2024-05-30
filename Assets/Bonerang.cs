using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonerang : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float rotateSpeed;
    private Vector3 target;
    public Vector3 Target
    {
        set {target = value;}
        get {return target;}

    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // move bonerang towards position given by ThrowBone.cs
        if(transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);

        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((enemyLayer.value & (1 << col.gameObject.layer)) > 0)
            Destroy(gameObject);
    }




}
