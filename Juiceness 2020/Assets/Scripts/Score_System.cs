﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_System : MonoBehaviour
{
    [SerializeField]
    public Text scoreText = null;
    public Text highscoreText = null;

    string highScoreKey = "HighScore";

    public int gameScore = 0;
    public int highScore = 0;
    public int whackPoints = 10;

    public void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highscoreText.text = highScore.ToString();
    }
    public void MolePoints()
    {
        int newscore = gameScore += whackPoints;
        scoreText.text = newscore.ToString();
    }
    void OnDisable()
    {

        //If our score is greter than highscore, set new higscore and save.
        if (gameScore > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, gameScore);
            PlayerPrefs.Save();
        }
    }

}
