using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSwitch : MonoBehaviour
{
    private bool interacted;
    public GameObject trapObject;
    public GameObject shadowObject;
    public float trapMoveSpeed;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!interacted)
        {
            interacted = true;

            StartCoroutine(SpawnTrap());
            

            
        }
    }

    private IEnumerator SpawnTrap()
    {
            // spawn trap and shadow
            var trap = Instantiate(trapObject, transform.position, Quaternion.identity);
            var shadow = Instantiate(shadowObject, transform.position, Quaternion.identity);

            // move trap down
            while(Vector3.Distance(trap.transform.position, shadowObject.transform.position) < 0.2f) // difference in distance should be tweaked with shadow
            {
                trap.transform.position = Vector2.MoveTowards(trap.transform.position, shadowObject.transform.position, trapMoveSpeed);

                yield return null;
            }

            // reaches shadow
            Destroy(shadow);
            trap.GetComponent<BoxCollider2D>().enabled = true;



            
    }
}
