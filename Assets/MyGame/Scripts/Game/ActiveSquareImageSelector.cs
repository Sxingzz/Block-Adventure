using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSquareImageSelector : MonoBehaviour
{
    public SquareTextureData squareTextureData;
    public bool updateImageOnRechedTreshold = false;

    private void OnEnable()
    {
        UpdateSquareColorBaseOnCurrentPoints();

        if (updateImageOnRechedTreshold)
            GameEvent.UpdateSquareColor += UpdateSquaresColor;
    }

    private void OnDisable()
    {
        if (updateImageOnRechedTreshold)
            GameEvent.UpdateSquareColor -= UpdateSquaresColor;
    }

    private void UpdateSquareColorBaseOnCurrentPoints()
    {
        foreach (var squareTexture in squareTextureData.activeSquareTextures)
        {
            if (squareTextureData.currentColor == squareTexture.squareColor)
            {
                GetComponent<Image>().sprite = squareTexture.textture;
            }
        }
    }

    private void UpdateSquaresColor(Config.SquareColor color)
    {
        foreach (var squareTexture in squareTextureData.activeSquareTextures)
        {
            if (color == squareTexture.squareColor)
            {
                GetComponent<Image>().sprite = squareTexture.textture;
            }
        }
    }

}
