using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI bomba;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
