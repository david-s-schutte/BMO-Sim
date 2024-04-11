using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerGoS player;
    UIManager uiManager;
    int score = 0;
    bool gameStarted = false;

    [Header("Parent Objects")]
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerGoS>();
        playerObj.SetActive(gameStarted);
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
        else if(Input.GetButtonDown("Submit") && gameStarted && player.GetHealth() < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(player.GetHealth() < 1)
        {
            gameOverScreen.SetActive(true);
            playerObj.SetActive(false);
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
}
