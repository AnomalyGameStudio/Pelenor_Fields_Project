﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    public int lives = 20;
    public int money = 100;

    public Text moneyText;
    public Text livesText;

    protected ScoreManager() {}

    public void LoseLife(int l=1)
    {
        lives -= l;

        if(lives <= 0)
        {
            GameOver();
        }
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");

        // TODO: Send the player to a game-over screen instead!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // FIXME: This Doesn't actually need to update the text every frame.
        moneyText.text = "Money: " + money;
        livesText.text = "Lives: " + lives;

    }
}
