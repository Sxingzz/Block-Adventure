using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Scores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int currentScores_;

    // Start is called before the first frame update
    void Start()
    {
        currentScores_ = 0;
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvent.AddScores += AddScores;
    }

    private void OnDisable()
    {
        GameEvent.AddScores -= AddScores;
    }

    private void AddScores(int scores)
    {
        currentScores_ += scores;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScores_.ToString();
    }

}
