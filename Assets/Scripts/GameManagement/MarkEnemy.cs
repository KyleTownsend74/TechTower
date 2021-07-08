using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If enemy is in the playing field, allow them to be targets of objects
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().MakeTargetable();
        }
    }
}
