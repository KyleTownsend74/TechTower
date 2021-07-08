using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int damage;
    private int piercing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Delete bullet if no more piercing left
            piercing--;
            if (piercing <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Check if bullet has left the map
        if(collision.gameObject.name == "MapBoundary")
        {
            Destroy(gameObject);
        }
    }

    public void SetStats(int damage, int piercing)
    {
        this.damage = damage;
        this.piercing = piercing;
    }

    public int GetDamage()
    {
        return damage;
    }
}
