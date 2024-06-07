using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BestScoreData
{
    public int score = 0;
}

public class Scores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private bool newBestScore_ = false;
    private BestScoreData bestScores_ = new BestScoreData();
    private int currentScores_;
    private string bestScoreKey_ = "bsdat";

    private void Awake()
    {
        if (BinaryDataStream.Exist(bestScoreKey_))
        {
            StartCoroutine(ReadDataFile());
        }
    }

    private IEnumerator ReadDataFile()
    {
        bestScores_ = BinaryDataStream.Read<BestScoreData>(bestScoreKey_);
        yield return new WaitForEndOfFrame();
        Debug.Log("read best score = " + bestScores_.score);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScores_ = 0;
        newBestScore_ = false;
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvent.AddScores += AddScores;
        GameEvent.Gameover += SaveBestScores;
    }

    private void OnDisable()
    {
        GameEvent.AddScores -= AddScores;
        GameEvent.Gameover -= SaveBestScores;
    }

    public void SaveBestScores(bool newBestScores)
    {
        BinaryDataStream.Save<BestScoreData>(bestScores_, bestScoreKey_);
    }

    private void AddScores(int scores)
    {
        currentScores_ += scores;
        if(currentScores_ > bestScores_.score)
        {
            newBestScore_ = true;
            bestScores_.score = currentScores_;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScores_.ToString();
    }

}
