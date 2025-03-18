using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText; // Reference to CurrentScore
    [SerializeField] private TextMeshProUGUI updatedScoreText; // Reference to UpdatedScore
    [SerializeField] private Transform scoreTextContainer;    // Reference to ScoreTextContainer
    [SerializeField] private float animationDuration = 0.4f;  // Duration of the slide
    [SerializeField] private Ease animationCurve = Ease.OutQuad; // Animation easing

    private float containerInitPosition;
    private float moveAmount;

    private void Start()
    {
        // Force UI update to ensure accurate RectTransform data
        Canvas.ForceUpdateCanvases();
        currentScoreText.SetText("0");
        updatedScoreText.SetText("0");
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = currentScoreText.rectTransform.rect.height;
    }

    public void UpdateScore(int score)
    {
        updatedScoreText.SetText(score.ToString());
        scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, animationDuration)
            .SetEase(animationCurve);
        StartCoroutine(ResetContainerAfterAnimation(score));
    }

    private System.Collections.IEnumerator ResetContainerAfterAnimation(int score)
    {
        yield return new WaitForSeconds(animationDuration);
        currentScoreText.SetText(score.ToString());
        Vector3 localPosition = scoreTextContainer.localPosition;
        scoreTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }
}