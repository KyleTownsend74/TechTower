using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    //Represent settings for each difficulty; array[0] for easy, [1] for normal, [2] for hard
    public double[] posMult;  //Higher => better for player; apply to start health, start currency, gaining currency
    public double[] negMult; //Higher => worse for player; apply to enemy health, object cost
    
    public Wave[,] rounds;

    public GameObject basicEnemy;
    public GameObject strongEnemy;
    public GameObject fastEnemy;
    public GameObject strongFastEnemy;
    public GameObject tankEnemy;

    public float spawnCooldown;

    //Generates and returns rounds for easy difficulty
    public Wave[,] genEasyRounds()
    {
        rounds = new Wave[5, 2];
        rounds[0, 0] = new Wave(basicEnemy, 3, spawnCooldown, 0f);
        rounds[0, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[1, 0] = new Wave(basicEnemy, 5, spawnCooldown, 4.8f);
        rounds[1, 1] = new Wave(basicEnemy, 5, spawnCooldown, 0f);

        rounds[2, 0] = new Wave(basicEnemy, 5, spawnCooldown, 6f);
        rounds[2, 1] = new Wave(fastEnemy, 5, spawnCooldown, 0f);

        rounds[3, 0] = new Wave(basicEnemy, 10, spawnCooldown, 8f);
        rounds[3, 1] = new Wave(tankEnemy, 1, 2f, 0f);

        rounds[4, 0] = new Wave(fastEnemy, 15, spawnCooldown, 12f);
        rounds[4, 1] = new Wave(strongEnemy, 5, spawnCooldown, 0f);

        return rounds;
    }

    //Generates and returns rounds for normal difficulty
    public Wave[,] genNormalRounds()
    {
        rounds = new Wave[10, 3];
        rounds[0, 0] = new Wave(basicEnemy, 5, spawnCooldown, 0f);
        rounds[0, 1] = new Wave(basicEnemy, 5, spawnCooldown, 0f);
        rounds[0, 2] = new Wave(basicEnemy, 5, spawnCooldown, 0f);

        rounds[1, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[1, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[1, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[2, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[2, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[2, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[3, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[3, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[3, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[4, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[4, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[4, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[5, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[5, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[5, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[6, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[6, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[6, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[7, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[7, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[7, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[8, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[8, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[8, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[9, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[9, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[9, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        return rounds;
    }

    //Generates and returns rounds for hard difficulty
    public Wave[,] genHardRounds()
    {
        rounds = new Wave[15, 3];
        rounds[0, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[0, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[0, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[1, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[1, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[1, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[2, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[2, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[2, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[3, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[3, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[3, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[4, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[4, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[4, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[5, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[5, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[5, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[6, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[6, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[6, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[7, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[7, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[7, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[8, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[8, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[8, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[9, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[9, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[9, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[10, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[10, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[10, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[11, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[11, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[11, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[12, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[12, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[12, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[13, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[13, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[13, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        rounds[14, 0] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[14, 1] = new Wave(basicEnemy, 0, spawnCooldown, 0f);
        rounds[14, 2] = new Wave(basicEnemy, 0, spawnCooldown, 0f);

        return rounds;
    }
}
