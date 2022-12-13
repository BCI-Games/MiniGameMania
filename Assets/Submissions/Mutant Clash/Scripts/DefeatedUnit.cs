using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedUnit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float pushStrength = 5;
    public float decayLerp = 0.05f;
    public float killThreshold = 0.1f;

    Vector2 velocity;

    public void Init(Vector2 position, Sprite sprite, Vector2 direction)
    {
        transform.position = position;

        spriteRenderer.sprite = sprite;

        velocity = direction.normalized + Vector2.up * Random.Range(-0.25f, 0.25f);
        velocity *= pushStrength;
    }

    private void Update()
    {
        transform.position += (Vector3)velocity;

        velocity = Vector2.Lerp(velocity, Vector2.zero, decayLerp);
        if (velocity.magnitude < killThreshold)
            Destroy(gameObject);
    }
}
