using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Objects")]
    public AudioSource objectPlace;
    public AudioSource notEnoughMoney;
    public AudioSource sell;
    public AudioSource cannon;
    public AudioSource sniper;

    [Header("Enemy")]
    public AudioSource enemyDamage;
    public AudioSource enemyDestroy;

    [Header("Player")]
    public AudioSource playerDamage;
    public AudioSource playerDestroy;
    public AudioSource playerWin;

    public void PlayObjectPlace()
    {
        objectPlace.Play();
    }

    public void PlayNotEnoughMoney()
    {
        notEnoughMoney.Play();
    }

    public void PlaySell()
    {
        sell.Play();
    }

    public void PlayCannon()
    {
        cannon.Play();
    }

    public void PlaySniper()
    {
        sniper.Play();
    }

    public void PlayEnemyDamage()
    {
        enemyDamage.Play();
    }

    public void PlayEnemyDestroy()
    {
        enemyDestroy.Play();
    }

    public void PlayPlayerDamage()
    {
        playerDamage.Play();
    }

    public void PlayPlayerDestroy()
    {
        playerDestroy.Play();
    }

    public void PlayPlayerWin()
    {
        playerWin.Play();
    }
}
