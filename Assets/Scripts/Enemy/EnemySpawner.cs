using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Spawns a single wave of a round
    private IEnumerator SpawnEnemies(Wave wave)
    {
        //Iterates over each wave within the round
        for (int i = 0; i < wave.numEnemies; i++)
        {
            Instantiate(wave.enemy, gameObject.transform);
            //Only wait for spawn cooldown if there are more enemies to spawn
            if (i != wave.numEnemies - 1)
            {
                yield return new WaitForSeconds(wave.spawnCooldown);
            }
        }
    }

    //Handles spawning a whole round of enemies - calls the SpawnEnemies function to actually spawn them
    //Use by getting a reference to the spawnpoint, then calling StartCoroutine(reference.GetComponent<EnemySpawner>().Spawn(Wave[]));
    public IEnumerator Spawn(Wave[] round)
    {
        for (int i = 0; i < round.Length; i++)
        {
            StartCoroutine(SpawnEnemies(round[i]));
            //Only wait for wave cooldown if there are waves left
            if (i != round.Length - 1)
            {
                yield return new WaitForSeconds(round[i].waveCooldown);
            }
        }
    }
}