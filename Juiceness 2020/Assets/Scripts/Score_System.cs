using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_System : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    Text highScoreText;

    int gameScore = 0;
    int highScore = 0;
    int whackPoints = 50;
    

    void Update()
    {
        scoreText.text = gameScore.ToString();

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameScore = gameScore+whackPoints;
        }
    }
}
