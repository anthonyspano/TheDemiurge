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

    public Transform owner;
    public bool isReturning;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // move bonerang towards position given by ThrowBone.cs
        if(isReturning)
            target = owner.position;
        if(transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);

        }

        if(owner)
            if(transform.position == owner.position)
                Destroy(gameObject);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if((enemyLayer.value & (1 << col.gameObject.layer)) > 0)
            Destroy(gameObject);
    }

    public void SetTarget(ref int pos)
    {
        Debug.Log(pos);
    }




}
