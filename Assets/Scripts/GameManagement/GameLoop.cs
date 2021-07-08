using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public static int numEnemiesLeft;

    private InGameUI gameUI;
    private int curRound;
    private bool fightInProgress;
    private bool timerInProgress;
    private float timer;

    public GameObject player;
    private int currencyToAdd;

    public int difficulty;  //0 for easy, 1 for normal, 2 for hard
    private Difficulty difficultyScript;

    public GameObject spawner;
    public Wave[,] rounds;

    public int secondsToBuild;
    public float secondsForRoundMessage;     // How long to display round messages and wait for next phase of round.

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameUI = gameObject.GetComponent<InGameUI>();
        curRound = 0;
        numEnemiesLeft = 0;
        fightInProgress = false;
        timerInProgress = false;
        timer = secondsToBuild;

        //Set up rounds (MOVED TO Difficulty.cs)
        /*rounds = new Wave[4, 1];
        rounds[0, 0] = new Wave(basicEnemy, 3, 1f, 0f);
        rounds[1, 0] = new Wave(basicEnemy, 10, 1f, 0f);
        rounds[2, 0] = new Wave(basicEnemy, 20, 1f, 0f);
        rounds[3, 0] = new Wave(strongEnemy, 3, 1f, 0f);*/

        //Difficulty modifications
        difficultyScript = GetComponent<Difficulty>();
        currencyToAdd = (int)(300 * difficultyScript.posMult[difficulty]);

        if (difficulty == 0)
        {
            rounds = difficultyScript.genEasyRounds();
        }
        else if(difficulty == 1)
        {
            rounds = difficultyScript.genNormalRounds();
        }
        else
        {
            rounds = difficultyScript.genHardRounds();
        }

        gameUI.UpdateWaveCounter(curRound + 1, rounds.GetLength(0));     // Set up initial wave UI.
        StartRound();
    }

    private void Update()
    {
        // If player is in fight phase and there are no more enemies, the round is over.
        if(fightInProgress && numEnemiesLeft <= 0)
        {
            EndRound();
        }

        // Timer for build phase. Once completed, fight phase starts.
        if (timerInProgress)
        {
            timer -= Time.deltaTime;
            gameUI.UpdateTimer(timer);

            if(timer <= 0)
            {
                timerInProgress = false;
                gameUI.AfterBuild(secondsForRoundMessage);
                StartCoroutine(WaitToFight(secondsForRoundMessage));   // Build phase is complete, start fight phase.
            }
        }
    }

    public void StartRound()
    {
        // Each round consists of build phase and fight phase. Fight phase called from Update when timer finishes.
        if (!InGameUI.gameOver)
        {
            BuildPhase();
        }
    }

    public void EndRound()
    {
        fightInProgress = false;

        if (!InGameUI.gameOver)
        {
            // If there are still more rounds to go, keep playing. Otherwise, the player has won.
            if (curRound < rounds.GetLength(0) - 1)
            {
                curRound++;
                timer = secondsToBuild;

                //Give player money, then increase amount to give them for the next round
                player.GetComponent<PlayerManager>().AddCurrency(currencyToAdd);
                currencyToAdd += 50;

                // Tell user the round is over before immediately starting the next round.
                gameUI.AfterRound(secondsForRoundMessage);
                StartCoroutine(WaitToStart(secondsForRoundMessage));
            }
            else
            {
                gameUI.EndGame(true);
            }
        }
    }

    // Phase of round for player to build up defenses on grid.
    private void BuildPhase()
    {
        timerInProgress = true;
    }

    // Phase of round for enemies to spawn and player defenses to fight off enemies.
    private void FightPhase()
    {
        gameUI.UpdateWaveCounter(curRound + 1, rounds.GetLength(0));   // Wave is updated at beginning of fight phase, not build phase.

        //Get current Wave[] to pass to spawner and calc num of enemies for the round
        Wave[] newRound = new Wave[rounds.GetLength(1)];
        for(int i = 0; i < rounds.GetLength(1); i++)
        {
            newRound[i] = rounds[curRound, i];
            numEnemiesLeft += rounds[curRound, i].numEnemies;
        }
        //Debug.Log("Enemies in round: " + numEnemiesLeft);
        StartCoroutine(spawner.GetComponent<EnemySpawner>().Spawn(newRound));

        fightInProgress = true;
    }

    private IEnumerator WaitToStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartRound();
    }

    private IEnumerator WaitToFight(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FightPhase();
    }
}
