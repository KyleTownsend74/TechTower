using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject gameManager;
    private SoundManager soundManager;
    private InGameUI gameUI;
    private int curHealth;
    private GameObject selectedObject;
    private int selectedObjectIndex;
    private TileInteract curTile;
    private int currency;
    private bool lost;
    private int currencyBonus;

    public int startHealth;
    public GameObject selectObject;
    public GameObject[] objects;
    public int startCurrency;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        soundManager = gameManager.GetComponent<SoundManager>();
        gameUI = gameManager.GetComponent<InGameUI>();
        curHealth = startHealth;
        selectedObjectIndex = 0;
        SetSelectedObject(1);       // Set selected object to object 1 (index 0).
        currency = startCurrency;
        lost = false;
        currencyBonus = 0;

        gameManager.GetComponent<InGameUI>().UpdateCurrency(currency);
    }

    public void TakeDamage(int dmg)
    {
        curHealth -= dmg;
        if(curHealth <= 0)
        {
            curHealth = 0; // Game should not show health going below 0. If health is at 0, player already lost.
            
            if(!lost)
            {
                gameUI.EndGame(false);
                lost = true;
            }
        }

        gameUI.UpdateHealth(curHealth);
        if (!lost)
        {
            soundManager.PlayPlayerDamage();
        }
    }

    public void SetSelectedObject(int num)
    {
        gameUI.UpdateSelectorUI(selectedObjectIndex, num - 1);
        selectedObjectIndex = num - 1;
        selectedObject = objects[num - 1];
        TileInteract.UpdateObject(selectedObject);

        if(curTile != null)
        {
            curTile.UpdateGhost();
        }
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void AddCurrency(int amount)
    {
        currency += amount + currencyBonus;
        gameUI.UpdateCurrency(currency);
    }

    public void DeductCurrency(int amount)
    {
        currency -= amount;
        gameUI.UpdateCurrency(currency);
    }

    public void AddToCurrencyBonus(int amount)
    {
        currencyBonus += amount;
    }

    public void DeductFromCurrencyBonus(int amount)
    {
        currencyBonus -= amount;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void SetCurTile(TileInteract tile)
    {
        curTile = tile;
    }
}
