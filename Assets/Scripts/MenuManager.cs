using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("UIs")]
    public RectTransform title;
    public Transform ball;
    public Button playButton;

    [Header("Ball Effect stats")]
    public float leftX = -3f;
    public float rightX = 3f;
    public float ballEffectDuration = 2f;
    public float waveHeight = 30f;
    public float titleEffectDuration = 0.5f;

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        //juice
        TitleEffect();
        BallMovement();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        GameManager.Instance.Splash();
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.StartGame();
    }

    void BallMovement()
    {
        ball.DOMoveX(rightX, ballEffectDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // Instantly reset to left, then move right again
                ball.position = new Vector3(leftX, ball.position.y, ball.position.z);
                BallMovement();
            });
    }

    void TitleEffect()
    {
        // Yoyo = up and down, Loop = infinite
        title.DOAnchorPosY(title.anchoredPosition.y + waveHeight, titleEffectDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
