using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBehaviour
{
    public float patrolSpeed, height, bumpLength, turnDelay;
    public Vector2 raycastPoint;
    public LayerMask ObstacleMask;
    public AudioPlayer footstepAudio;

    float direction{get=>transform.localScale.x; set=>transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);}

    public void StartPatrol()
    {
        turnCooldown = 0f;
        if(Turn())
        {
            direction *= -1f;
        }
    }

    void Update()
    {
        Patrol();
    }

    float turnCooldown;
    void Patrol()
    {
        if(turnCooldown > 0f)
        {
            turnCooldown -= Time.deltaTime;
            if(turnCooldown <= 0f)
            {
                direction *= -1f;
            }
            return;
        }

        transform.position += Vector3.right * direction * patrolSpeed * Time.deltaTime;
        footstepAudio.Play();

        if(Turn())
        {
            turnCooldown = turnDelay;
        }
    }

    bool Turn()
    {
        if(Physics2D.Raycast((Vector2)transform.position + raycastPoint, new Vector2(direction, 0f), bumpLength, ObstacleMask))
            return true;
        Vector2 edge = (Vector2)transform.position + raycastPoint + new Vector2(direction, 0f) * enemy.halfWidth;
        if(!Physics2D.Raycast(edge, Vector2.down, height, ObstacleMask))
            return true;
        return false;
    }
}
