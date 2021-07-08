using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerAI : ObjectAttributes
{
    private List<EnemyController> enemies;
    private bool shouldSlow;

    private void Start()
    {
        enemies = new List<EnemyController>();
        shouldSlow = true;

        gameObject.GetComponent<CircleCollider2D>().radius = radiusOfEffect;
    }

    private void Update()
    {
        // Slow in-range enemies down (need to do it in Update to account for overlapping object radii
        if(enemies.Count > 0 && shouldSlow)
        {
            StartCoroutine(SlowEnemies());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // Add enemy as an enemy to slow
            enemies.Add(collision.GetComponent<EnemyController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // Remove enemy from enemies to slow
            enemies.Remove(collision.GetComponent<EnemyController>());

            // Reset enemy to its start speed
            collision.GetComponent<EnemyController>().ResetSpeed();
        }
    }

    private IEnumerator SlowEnemies()
    {
        // Slow down each in-range enemy based on start speed
        foreach(EnemyController enemy in enemies)
        {
            if(enemy.IsTargetable())
            {
                enemy.SetCurSpeed(enemy.GetStartSpeed() * 0.65f);
            }
        }

        // Delay so that enemies are not slowed down every frame
        shouldSlow = false;
        yield return new WaitForSeconds(0.1f);
        shouldSlow = true;
    }
}
