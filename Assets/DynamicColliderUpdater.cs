using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicColliderUpdater : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Awake()
    {
        // Get references to the required components
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void LateUpdate()
    {
        // Check if the sprite has changed
        if (spriteRenderer.sprite != null)
        {
            UpdateColliderShape();
        }
    }

    void UpdateColliderShape()
    {
        // Clear existing collider paths
        polygonCollider.pathCount = 0;

        // Get the sprite's physics shape (its outline)
        List<Vector2> spriteOutline = new List<Vector2>();
        int shapeCount = spriteRenderer.sprite.GetPhysicsShape(0, spriteOutline);

        // Set the collider's path based on the outline
        polygonCollider.SetPath(0, spriteOutline);
    }
}

