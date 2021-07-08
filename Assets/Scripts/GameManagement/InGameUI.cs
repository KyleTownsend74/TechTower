using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public static bool isPaused;
    public static bool gameOver;

    // Variables related to making health bar functional
    private PlayerManager playerManager;
    private RectTransform barRect;
    private float height;
    private float startWidth;
    private int maxHealth;

    // Variables related to making not enough money animation functional
    private float currencyColorDuration;
    private bool isAnimating;

    [Header("UI Objects")]
    public GameObject inGameUI;
    public GameObject pauseUI;
    public GameObject helpUI;
    public GameObject roundMessageUI;
    public GameObject endGameUI;

    [Header("UI Adjustment Elements")]
    public GameObject healthBar;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI roundMessageText;
    public TextMeshProUGUI endGameText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;

    [Header("UI Frame Sprites")]
    public Sprite unselected;
    public Sprite selected;

    [Header("UI Frame Objects")]
    public GameObject[] objectFrames;

    private void Start()
    {
        isPaused = false;
        gameOver = false;

        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        barRect = healthBar.GetComponent<RectTransform>();
        height = barRect.rect.height;
        startWidth = barRect.rect.width;
        maxHealth = playerManager.startHealth;
        currencyColorDuration = 0.25f;
        isAnimating = false;
    }

    public void ToggleInGameUI()
    {
        inGameUI.SetActive(!inGameUI.activeSelf);
    }

    public void TogglePauseUI()
    {
        // If game is not over, allow user to pause / unpause.
        if (!gameOver)
        {
            // If not paused, pause.
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        helpUI.SetActive(false);    // If helpUI is shown in paused state, hide it.
    }

    public void Help()
    {
        pauseUI.SetActive(false);
        helpUI.SetActive(true);
    }

    public void HelpBack()
    {
        pauseUI.SetActive(true);
        helpUI.SetActive(false);
    }

    public void QuitGame()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void AfterRound(float seconds)
    {
        roundMessageUI.SetActive(true);
        roundMessageText.text = ("Wave Complete!");
        StartCoroutine(AfterRoundComplete(seconds));
    }

    public void AfterBuild(float seconds)
    {
        roundMessageUI.SetActive(true);
        roundMessageText.text = ("Wave Started!");
        StartCoroutine(AfterRoundComplete(seconds));
    }

    public void EndGame(bool won)
    {
        gameOver = true;
        isPaused = true; // Game is no longer running, tell other scripts to not let user interact with game.
        endGameUI.SetActive(true);

        if (won)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void UpdateCurrency(int amount)
    {
        currencyText.text = ("$" + amount);
    }

    public void StartCurrencyFlash()
    {
        if(!isAnimating)
        {
            StartCoroutine(FlashCurrency());
        }
    }

    public void UpdateHealth(int health)
    {
        float percent = (float)health / maxHealth;
        barRect.sizeDelta = new Vector2(percent * startWidth, height);
    }

    public void UpdateTimer(float curTime)
    {
        int minutes = (int)curTime / 60;
        int seconds = (int)curTime - (60 * minutes);

        // If user has NOT hidden the timer, update it.
        if (inGameUI.activeSelf)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void UpdateWaveCounter(int curRound, int maxRound)
    {
        waveText.text = curRound + "/" + maxRound;
    }

    public void UpdateSelectorUI(int oldObject, int curObject)
    {
        objectFrames[oldObject].GetComponent<Image>().sprite = unselected;
        objectFrames[curObject].GetComponent<Image>().sprite = selected;
    }

    private void WinGame()
    {
        endGameText.text = ("You won!");
        gameObject.GetComponent<SoundManager>().PlayPlayerWin();
    }

    private void LoseGame()
    {
        endGameText.text = ("You lost!");
        gameObject.GetComponent<SoundManager>().PlayPlayerDestroy();
    }

    private IEnumerator AfterRoundComplete(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        roundMessageUI.SetActive(false);
    }

    private IEnumerator FlashCurrency()
    {
        isAnimating = true;

        float timer = 0f;
        Color startColor = currencyText.faceColor;
        Color endColor = new Color(1f, 0f, 0f, 1f);

        // Text color animation cycle
        while(timer < currencyColorDuration)
        {
            currencyText.faceColor = Color.Lerp(startColor, endColor, timer / currencyColorDuration);
            timer += Time.deltaTime;

            yield return null;
        }
        currencyText.faceColor = endColor;
        timer = 0f;
        while (timer < currencyColorDuration)
        {
            currencyText.faceColor = Color.Lerp(endColor, startColor, timer / currencyColorDuration);
            timer += Time.deltaTime;

            yield return null;
        }
        currencyText.faceColor = startColor;
        timer = 0f;
        while (timer < currencyColorDuration)
        {
            currencyText.faceColor = Color.Lerp(startColor, endColor, timer / currencyColorDuration);
            timer += Time.deltaTime;

            yield return null;
        }
        currencyText.faceColor = endColor;
        timer = 0f;
        while (timer < currencyColorDuration)
        {
            currencyText.faceColor = Color.Lerp(endColor, startColor, timer / currencyColorDuration);
            timer += Time.deltaTime;

            yield return null;
        }
        currencyText.faceColor = startColor;

        isAnimating = false;
    }
}
