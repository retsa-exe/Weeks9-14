using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    //input game over ui and player
    public GameObject gameOver;
    public Player player;

    //functions controls the visibility of game over UI
    public void hideGameOver()
    {
        gameOver.SetActive(false);
    }

    public void showGameOver()
    {
        gameOver.SetActive(true);
    }

    //add listener to game over event
    private void Start()
    {
        player.onGameOver.AddListener(showGameOver);
    }
}
