using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAI : ObjectAttributes
{
    private PlayerManager playerManager;

    public int currencyBonus;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        playerManager.AddToCurrencyBonus(currencyBonus);
    }

    public void RemoveBonus()
    {
        playerManager.DeductFromCurrencyBonus(currencyBonus);
    }
}
