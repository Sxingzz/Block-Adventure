using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreBar : MonoBehaviour
{
    public Image fillInImage;
    public TextMeshProUGUI bestScoreText;

    private void OnEnable()
    {
        GameEvent.UpdateBestScoreBar += UpdateBestScoreBar;
    }

    private void OnDisable()
    {
        GameEvent.UpdateBestScoreBar -= UpdateBestScoreBar;
    }

    private void UpdateBestScoreBar(int currentScore, int bestScore)
    {
        float currentPrecentage = (float) currentScore / (float) bestScore;
        fillInImage.fillAmount = currentPrecentage;
        bestScoreText.text = bestScore.ToString();
    }


}
