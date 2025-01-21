using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTestScript : MonoBehaviour
{

    public Transform player; // Reference to the player's Transform
    public float rotationSpeed = 5f; // Speed of rotation

    private void Update()
    {
        // Get stick input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if there's any stick input
        if (horizontal != 0 || vertical != 0)
        {
            // Calculate the target angle from stick input
            float targetAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

            // Get the current angle of the object relative to the player
            Vector2 directionToObject = transform.position - player.position;
            float currentAngle = Mathf.Atan2(directionToObject.y, directionToObject.x) * Mathf.Rad2Deg;

            // Calculate the angle difference
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

            // Rotate the object around the player
            float rotationStep = rotationSpeed * Time.deltaTime * Mathf.Sign(angleDifference);
            if (Mathf.Abs(rotationStep) > Mathf.Abs(angleDifference))
            {
                rotationStep = angleDifference; // Snap to target angle if close enough
            }

            transform.RotateAround(player.position, Vector3.forward, rotationStep);
        }
    }


}
