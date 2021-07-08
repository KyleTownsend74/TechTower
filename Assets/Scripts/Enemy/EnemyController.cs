using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References to Use")]
    public Rigidbody2D rb;
    public GameObject particles;

    [Header("Attributes")]
    public float startSpeed;
    public float health;
    public int killReward;
    public int damage;

    private SoundManager soundManager;
    private GameObject player;
    private Vector2 direction;
    private float curSpeed;
    private bool targetable;
    private bool isAlive;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>();
        direction = Vector2.right;
        player = GameObject.FindWithTag("Player");
        curSpeed = startSpeed;
        targetable = false;
        isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Deplete health and possibly destroy if hit with a bullet
        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<BulletManager>().GetDamage();
            if (health <= 0 && isAlive)
            {
                //Enemy dies
                isAlive = false;
                soundManager.PlayEnemyDestroy();
                Die();
                player.GetComponent<PlayerManager>().AddCurrency(killReward);
            }
            else if (isAlive)
            {
                soundManager.PlayEnemyDamage();
            }
        }
        //Adjust movement along path
        else if (collision.gameObject.tag == "CornerUp")
        {
            direction = Vector2.up;
        }
        else if(collision.gameObject.tag == "CornerDown")
        {
            direction = Vector2.down;
        }
        else if(collision.gameObject.tag == "CornerLeft")
        {
            direction = Vector2.left;
        }
        else if(collision.gameObject.tag == "CornerRight")
        {
            direction = Vector2.right;
        }
        //Destroy at path end
        else if(collision.gameObject.name == "PathEnd")
        {
            player.GetComponent<PlayerManager>().TakeDamage(damage);
            Die();
        }
    }

    private void FixedUpdate()
    {
        //Move enemy
        rb.MovePosition(rb.position + direction * curSpeed * Time.fixedDeltaTime);
    }

    private void Die()
    {
        GameLoop.numEnemiesLeft--;
        Instantiate(particles, new Vector3(
                    gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                    Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetStartSpeed()
    {
        return startSpeed;
    }

    public void SetCurSpeed(float speed)
    {
        curSpeed = speed;
    }

    public void ResetSpeed()
    {
        curSpeed = startSpeed;
    }

    public void MakeTargetable()
    {
        targetable = true;
    }

    public bool IsTargetable()
    {
        return targetable;
    }
}
