using System.Collections;
using System.Collections.Generic;
using CardMatch.Score;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
        if (scoreSystem != null)
        {
            scoreSystem.OnScoreChanged += UpdateScore;
            UpdateScore(scoreSystem.CurrentScore);
        }
    }

    public void UpdateScore(int newScore)
    {
        if (scoreText != null)
            scoreText.text = newScore.ToString();
    }
}
