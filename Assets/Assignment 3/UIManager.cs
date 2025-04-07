using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //UI components
    public TextMeshProUGUI scoreText;
    public Slider healthBar;

    //player reference
    public Player player;

    private void Start()
    {
        //add listener
        player.onScoreChanged.AddListener(updateScore);
        player.onHealthChanged.AddListener(updateHealth);

        //initiallize the data from player
        updateHealth(player.health);
        updateScore(player.score);
    }

    public void updateScore(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void updateHealth(float health)
    {
        healthBar.value = health;
    }
}
