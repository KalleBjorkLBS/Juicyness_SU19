using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_System : MonoBehaviour
{
    [SerializeField]
    public Text scoreText = null;
    public Text highScoreText = null;

    public int gameScore = 0;
    public int highScore = 0;
    public int whackPoints = 50;
    

    public void MolePoints()
    {
        scoreText.text = gameScore.ToString();
        gameScore = gameScore + whackPoints;
    }
}
