using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitions : MonoBehaviour
{
    // MainUI includes buttons and title of UI, NOT background image.
    // Therefore, cannot use a single parent object of all UI.
    public GameObject[] mainUI;
    public GameObject helpUI;

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void Help()
    {
        helpUI.SetActive(true);
        
        for(int i = 0; i < mainUI.Length; i++)
        {
            mainUI[i].SetActive(false);
        }
    }

    public void HelpBack()
    {
        helpUI.SetActive(false);

        for (int i = 0; i < mainUI.Length; i++)
        {
            mainUI[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Successfully quit");
        Application.Quit();
    }
}
