using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    public Text highscoreText = null;

    public int highScore;
    string highScoreKey = "HighScore";

    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        //use this value in whatever shows the leaderboard.
        highscoreText.text = highScore.ToString();
    }

}