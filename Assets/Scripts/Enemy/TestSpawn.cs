using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public GameObject spawner;
    public GameObject enemy;

    public Wave w1;
    public Wave w2;
    public Wave w3;
    public Wave[] round;

    private void Awake()
    {
        w1 = new Wave(enemy, 2, .5f, 1f);
        w2 = new Wave(enemy, 2, .5f, 1f);
        w3 = new Wave(enemy, 2, .5f, 1f);
        round = new Wave[] { w1, w2, w3 };
    }

    private void OnMouseDown()
    {
        StartCoroutine(spawner.GetComponent<EnemySpawner>().Spawn(round));
    }
}
