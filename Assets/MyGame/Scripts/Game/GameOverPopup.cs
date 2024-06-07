using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public GameObject gameOverPopup;
    public GameObject losePopup;
    public GameObject newBestScorePopup;


    // Start is called before the first frame update
    void Start()
    {
        gameOverPopup.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvent.Gameover += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvent.Gameover -= OnGameOver;
    }

    private void OnGameOver(bool newBestScore)
    {
        gameOverPopup.SetActive(true);
        losePopup.SetActive(false) ;
        newBestScorePopup.SetActive(true);
    }
}
