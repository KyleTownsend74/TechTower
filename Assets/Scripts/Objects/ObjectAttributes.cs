using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttributes : MonoBehaviour
{
    public int buyPrice;
    public int damage;
    public int piercing;
    public float radiusOfEffect;      // Set radiusOfEffect to 0 if object does not require a radius.
    public int projectileSpeed;
    public float cooldown;

    public void SetRadius()
    {
        if (radiusOfEffect > 0)
        {
            transform.GetChild(0).localScale = new Vector3(radiusOfEffect * 2, radiusOfEffect * 2);
        }
    }

    public void DisableRadius()
    {
        if(radiusOfEffect > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
