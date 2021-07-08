using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAI : ObjectAttributes
{
    private ObjectAttributes attributes;
    private bool canShoot;
    private List<GameObject> enemies;
    private List<GameObject> targetableEnemies;
    private SoundManager soundManager;

    public GameObject projectile;

    private void Start()
    {
        attributes = gameObject.GetComponent<ObjectAttributes>();
        canShoot = true;
        enemies = new List<GameObject>();
        targetableEnemies = new List<GameObject>();
        soundManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>();

        gameObject.GetComponent<CircleCollider2D>().radius = attributes.radiusOfEffect;
    }

    private void Update()
    {
        if (canShoot && enemies.Count > 0)
        {
            // Reset the previous targetable enemies to get ready for a new set of in range enemies
            targetableEnemies.Clear();

            GameObject enemyToTarget = null;
            float minDistance = attributes.radiusOfEffect;
            Vector2 objectPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            // Picks closest enemy as the target enemy
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject curEnemy = enemies[i];

                if (curEnemy.GetComponent<EnemyController>().IsTargetable())
                {
                    targetableEnemies.Add(curEnemy);

                    Vector2 curEnemyPosition = new Vector2(curEnemy.transform.position.x, curEnemy.transform.position.y);
                    float curDistance = Vector2.Distance(curEnemyPosition, objectPosition);

                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        enemyToTarget = curEnemy;
                    }
                }
            }

            // If there is at least one targetable enemy, shoot
            if (targetableEnemies.Count > 0)
            {
                // If enemyToTarget is still null (in range but center of enemy (where distance is calculated) is
                // still out of range), use the first targetable enemy
                if (enemyToTarget == null)
                {
                    enemyToTarget = targetableEnemies[0];
                }

                StartCoroutine(Shoot(enemyToTarget, objectPosition));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy enters radius of effect
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Enemy leaves radius of effect
        if (collision.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
        }
    }

    private IEnumerator Shoot(GameObject enemyToTarget, Vector2 objectPosition)
    {
        canShoot = false;

        Vector2 enemyPosition = new Vector2(enemyToTarget.transform.position.x, enemyToTarget.transform.position.y);

        // Shoot projectile
        Vector2 direction = (enemyPosition - objectPosition).normalized;
        GameObject curProjectile = Instantiate(projectile, new Vector3(
            gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
            Quaternion.identity);
        curProjectile.GetComponent<BulletManager>().SetStats(attributes.damage, attributes.piercing);
        curProjectile.GetComponent<Rigidbody2D>().velocity = direction * attributes.projectileSpeed;

        // Play sound
        soundManager.PlaySniper();

        // Activate shoot cooldown
        yield return new WaitForSeconds(attributes.cooldown);
        canShoot = true;
    }
}
