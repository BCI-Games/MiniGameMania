using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    public UnitStats stats;

    bool movingLeft;

    [Header("sprites")]
    public Sprite idleSprite;
    public Sprite pushAnticipationSprite;
    public Sprite pushSprite;
    public Sprite defeatedSprite;

    public GameObject defeatedUnitPrefab;

    public Vector2 position => transform.position;

    SpriteRenderer spriteRenderer;

    int health;

    float idleResetTimer;

    public void Init(bool goLeft, Color colour)
    {
        health = stats.health;
        movingLeft = goLeft;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
        spriteRenderer.flipX = !movingLeft;
        spriteRenderer.color = colour;
    }

    public UnitBehavior MoveAndCollide(float baseMoveSpeed, List<UnitBehavior> lane, float collisionDistance)
    {
        Vector2 baseVelocity = Time.deltaTime * baseMoveSpeed *
            (movingLeft ? Vector2.left : Vector2.right);

        foreach(UnitBehavior unit in lane)
        {
            if(unit != this)
            {
                if (IsCollidingWith(baseVelocity, unit, collisionDistance))
                {
                    if (movingLeft == unit.movingLeft)
                    {
                        RunIdleReset();
                        return null;
                    }

                    idleResetTimer = 0.25f;
                    return unit;
                }
            }
        }

        RunIdleReset();

        transform.position = position + baseVelocity * stats.speed;
        return null;
    }

    public bool IsCollidingWith(Vector2 velocity, UnitBehavior unit, float collisionDistance)
    {
        if (!(Vector2.Distance(position + velocity, unit.position) < collisionDistance))
            return false;

        if (unit.movingLeft != movingLeft)
            return true;

        return (unit.position - position).normalized == velocity.normalized;
    }

    void RunIdleReset()
    {
        idleResetTimer -= Time.deltaTime;
        if (idleResetTimer <= 0)
            spriteRenderer.sprite = idleSprite;
    }

    public void SetSprite(UnitSpriteState spriteState)
    {
        if (!spriteRenderer)
            return;

        switch (spriteState)
        {
            case UnitSpriteState.Idle:
                spriteRenderer.sprite = idleSprite;
                break;
            case UnitSpriteState.Windup:
                spriteRenderer.sprite = pushAnticipationSprite;
                break;
            case UnitSpriteState.Push:
                spriteRenderer.sprite = pushSprite;
                break;
            case UnitSpriteState.Sit:
                spriteRenderer.sprite = defeatedSprite;
                break;
        }
    }

    public bool TakeDamage()
    {
        health -= 1;

        if(health <= 0)
        {
            OnDeath();
            return true;
        }
        return false;
    }

    void OnDeath()
    {
        DefeatedUnit defeatedInstance = Instantiate(defeatedUnitPrefab).GetComponent<DefeatedUnit>();
        defeatedInstance.Init(position, defeatedSprite, movingLeft ? Vector2.right : Vector2.left);

        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    health -= 1;
    //    if(health <= 0)
    //    {
    //        // get pushed back, play sit animation, disable collision and start death coroutine
    //        Destroy(gameObject);
    //    }
    //}
}

public enum UnitSpriteState
{
    Idle,
    Windup,
    Push,
    Sit
}