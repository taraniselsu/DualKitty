using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZoomiesHUD : MonoBehaviour
{
    [SerializeField] RectTransform energyFill;
    [SerializeField] Image energyFillImage;
    [SerializeField] TextMeshProUGUI scoreLabel;

    float[] energyFillColorBreaks = {0f, 0.25f, 0.5f, 1f};
    Color[] energyFillColors = {Color.red, Color.yellow, Color.green};

    private const float ANCHOR_MIN_X = 0.5f;
    private const float ANCHOR_MAX_X_FULL = 1f;
    private const float ANCHOR_MAX_Y = 0.95f;

    public void SetScore(int score) {
        scoreLabel.text = $"Score: {score}";
    }

    public void SetEnergyFill(float percentFullFraction) {
        Assert.IsTrue(0f <= percentFullFraction && percentFullFraction <= 1f, $"percentFull is {percentFullFraction} but should be between 0 incl and 1 incl");

        var anchorFullLength = ANCHOR_MAX_X_FULL - ANCHOR_MIN_X;
        var barLength = anchorFullLength * percentFullFraction;
        var currAnchorMaxX = ANCHOR_MIN_X + barLength;
        energyFill.anchorMax = new Vector2(currAnchorMaxX, ANCHOR_MAX_Y);

        Assert.IsTrue(energyFillColors.Length == energyFillColorBreaks.Length - 1, "Energy fill color-related arrays are the wrong relative length");
        Assert.IsTrue(energyFillColors.Length >= 1, "energyFillColors must have at least one element");
        for (int i=0; i<energyFillColors.Length; i++) {
            if (energyFillColorBreaks[i] <= percentFullFraction
                && percentFullFraction <= energyFillColorBreaks[i+1])
            {
                energyFillImage.color = energyFillColors[i];
                break;
            }
        }
    }
}
