using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    PlayerGoS player;
    UIManager uiManager;
    BGMManager bgmManager;
    int score = 0;
    bool gameStarted = false;
    int bossTotal;

    [Header("Parent Objects")]
    [SerializeField] GameObject playerObj;
    [Header("UI")]
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        bossTotal = 3;
        uiManager = GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerGoS>();
        playerObj.SetActive(gameStarted);
        bgmManager = GetComponent<BGMManager>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && !gameStarted)
        {
            gameStarted = true;
            titleScreen.SetActive(false);
            playerObj.SetActive(true);
        }
        else if(Input.GetButtonDown("Submit") && gameStarted && (player.GetHealth() < 1 || bossTotal <= 0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Input.GetButtonDown("Pause") && player.GetHealth() > 0)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        pauseMenu.SetActive(Time.timeScale == 0);

        if(player.GetHealth() < 1)
        {
            uiManager.UpdateEndGameText("You" + "\n" + "Lose", "Press Space to Try Again.");
            gameOverScreen.SetActive(true);
            playerObj.SetActive(false);
            bgmManager.StopBGM();
        }
        else if(bossTotal <= 0)
        {
            uiManager.UpdateEndGameText("You" + "\n" + "Win", "The Sunshine has been Saved! Press Space to Play Again!");
            gameOverScreen.SetActive(true);
            playerObj.SetActive(false);
            bgmManager.StopBGM();
        }

        if (!gameStarted)
            return;

        if (player.GetHealth() >= 0)
        {
            uiManager.UpdateLivesText(player.GetHealth());
            uiManager.UpdateBombaText(player.GetHasBomba());
            uiManager.UpdateScoreText(score);
        }
    }

    public void AddScore(int addition)
    {
        score += addition;
    }

    public void RemoveBoss()
    {
        bossTotal--;
    }
}
