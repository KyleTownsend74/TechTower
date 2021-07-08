using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public GameObject enemy;    //enemy GameObject to spawn
    public int numEnemies;      //number of enemies in the wave
    public float spawnCooldown; //seconds to wait spawn the next enemy in the wave
    public float waveCooldown;  //seconds to wait to spawn the next wave in the round, measured from the start of the wave

    public Wave(GameObject enemy, int numEnemies, float spawnCooldown, float waveCooldown)
    {
        this.enemy = enemy;
        this.numEnemies = numEnemies;
        this.spawnCooldown = spawnCooldown;
        this.waveCooldown = waveCooldown;
    }
}