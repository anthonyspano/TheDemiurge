using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    Vector2 currentInputVector;
    public float moveSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position = Vector2.MoveTowards(transform.position, 
                                                (Vector2)transform.position + currentInputVector, 
                                                moveSpeed * Time.deltaTime);
    }
}
