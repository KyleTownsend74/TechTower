using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private GameObject gameManager;
    private PlayerManager playerManager;

    public KeyCode toggleInGameUI;
    public KeyCode togglePauseUI;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerManager = gameObject.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleInGameUI))
        {
            gameManager.GetComponent<InGameUI>().ToggleInGameUI();
        }

        if (Input.GetKeyDown(togglePauseUI))
        {
            gameManager.GetComponent<InGameUI>().TogglePauseUI();
        }

        // Only allow player to change objects when game is not paused
        if (!InGameUI.isPaused)
        {
            if (Input.GetKeyDown("1"))
            {
                playerManager.SetSelectedObject(1);
            }

            if (Input.GetKeyDown("2"))
            {
                playerManager.SetSelectedObject(2);
            }

            if (Input.GetKeyDown("3"))
            {
                playerManager.SetSelectedObject(3);
            }

            if (Input.GetKeyDown("4"))
            {
                playerManager.SetSelectedObject(4);
            }

            if (Input.GetKeyDown("5"))
            {
                playerManager.SetSelectedObject(5);
            }
        }
    }
}
