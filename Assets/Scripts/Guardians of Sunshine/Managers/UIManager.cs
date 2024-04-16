using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI bomba;
    [SerializeField] Slider volume;
    [SerializeField] TextMeshProUGUI endStae;
    [SerializeField] TextMeshProUGUI endMessage;
    [SerializeField] Texture2D cursor;


    BGMManager bgmManager;
    
    void Start()
    {
        bgmManager = GetComponent<BGMManager>();
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();
    }

    public void UpdateLivesText(int livesLeft)
    {
        lives.text = "x " + livesLeft;
    }

    public void UpdateBombaText(bool hasBomba)
    {
        bomba.text = "x " + (hasBomba ? 1 : 0);
    }

    public void UpdateScoreText(int totalScore)
    {
        score.text = "" + totalScore;
    }

    public void UpdateVolume()
    {
        bgmManager.SetVolume(volume.value);
    }

    public void UpdateEndGameText(string state, string message)
    {
        endStae.text = state;
        endMessage.text = message;
    }
}
