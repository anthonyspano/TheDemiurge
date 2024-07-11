using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class FollowPlayer : MonoBehaviour
    {
        public float MoveSpeed;
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.Instance.transform.position, MoveSpeed * Time.deltaTime);        
        }
    }

}