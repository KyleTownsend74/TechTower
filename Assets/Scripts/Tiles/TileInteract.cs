using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteract : MonoBehaviour
{
    private static GameObject objectToPlace;

    private PlayerManager playerManager;
    private SoundManager soundManager;
    private InGameUI inGameUI;
    private Color startColor;
    private bool selected;
    private GameObject curObject;
    private GameObject objectGhost;

    public Color defaultTintColor;
    public Color deleteTintColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<SpriteRenderer>().color;
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        soundManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>();
        inGameUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<InGameUI>();
        selected = false;
        curObject = null;
    }

    private void OnMouseDown()
    {
        if (selected && !InGameUI.isPaused)
        {
            // If tile is empty, place object. Otherwise, delete object.
            if (curObject == null)
            {
                // Only place object if not using the select object.
                if (!objectToPlace.Equals(playerManager.selectObject))
                {
                    PlaceObject();
                }
            }
            else
            {
                // Only delete object if using the select object.
                if (objectToPlace.Equals(playerManager.selectObject))
                {
                    DeleteObject();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!InGameUI.isPaused)
        {
            playerManager.SetCurTile(this);
            // Old place for updating objectToPlace.
            // If tile has an object on it, choose different color.
            if (curObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = deleteTintColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = defaultTintColor;
                SpawnGhost();
            }

            selected = true;
        }
    }

    private void OnMouseExit()
    {
        playerManager.SetCurTile(null);
        gameObject.GetComponent<SpriteRenderer>().color = startColor;
        Destroy(objectGhost);
        objectGhost = null;
        selected = false;
    }

    private void PlaceObject()
    {
        //Check if player has enough money to buy object
        if (playerManager.GetCurrency() - objectToPlace.GetComponent<ObjectAttributes>().buyPrice >= 0)
        {
            // Old place for updating objectToPlace
            playerManager.DeductCurrency(objectToPlace.GetComponent<ObjectAttributes>().buyPrice);
            curObject = objectGhost;
            objectGhost = null;
            curObject.GetComponent<ObjectAttributes>().enabled = true;
            curObject.GetComponent<ObjectAttributes>().DisableRadius();

            // Mouse already inside tile, update color without OnMouseEnter or OnMouseExit
            gameObject.GetComponent<SpriteRenderer>().color = deleteTintColor;

            // Play place sound
            soundManager.PlayObjectPlace();
        }
        else    // If player does not have enough money, notify them through text color animation and sound
        {
            inGameUI.StartCurrencyFlash();
            soundManager.PlayNotEnoughMoney();
        }
    }

    private void DeleteObject()
    {
        // Check if object was a server. If so, remove currency bonus
        ServerAI server = curObject.GetComponent<ServerAI>();
        if(server != null)
        {
            server.RemoveBonus();
        }

        // Sell object for 50% of buy price
        int sellPrice = curObject.GetComponent<ObjectAttributes>().buyPrice / 2;
        playerManager.AddCurrency(sellPrice);

        Destroy(curObject);
        curObject = null;
        SpawnGhost();

        // Mouse already inside tile, update color without OnMouseEnter or OnMouseExit
        gameObject.GetComponent<SpriteRenderer>().color = defaultTintColor;

        // Play sell sound
        soundManager.PlaySell();
    }

    private void SpawnGhost()
    {
        objectGhost = Instantiate(objectToPlace, new Vector3(
            gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
            Quaternion.identity);
        objectGhost.GetComponent<ObjectAttributes>().SetRadius();
    }

    public static void UpdateObject(GameObject obj)
    {
        objectToPlace = obj;
    }

    public void UpdateGhost()
    {
        if (objectGhost != null)
        {
            Destroy(objectGhost);
            SpawnGhost();
        }
    }
}
