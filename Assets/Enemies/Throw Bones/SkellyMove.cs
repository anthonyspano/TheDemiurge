using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SkellyMove : MonoBehaviour
    {

        private void FixedUpdate()
        {        

            // if too close to player, move away
            if(Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < 5)
            {
                Vector2 directionToPlayer = (PlayerManager.Instance.transform.position - transform.position).normalized;
                Debug.Log(directionToPlayer);
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + directionToPlayer * new Vector2(-1,-1), 0.1f);
            }
            // else if too far, move closer
            else if(Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) > 10)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlayerManager.Instance.transform.position, 0.1f);
            }


        }

    }
}
