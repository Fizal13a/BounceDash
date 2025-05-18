using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Game Screen UIs")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    [Header("End Screen UIs")]
    public GameObject gameOverUI;
    public TextMeshProUGUI endCoinText;
    public TextMeshProUGUI endTimerText;
    public Button tryAgainBtn;
    public Button menuBtn;
    public Button quitBtn;

    private float gameTimer;
    private bool gameOver;

    private void OnEnable()
    {
        GameManager.Instance.OnCoinsCollected += UpdateCoinUI;
        GameManager.Instance.OnGameOver += GameOver;
    }

    private void Start()
    {
        tryAgainBtn.onClick.AddListener(GameManager.Instance.Restart);
        menuBtn.onClick.AddListener(GameManager.Instance.BackToMenu);
        quitBtn.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    private void Update()
    {
        if(!gameOver)
        {
            gameTimer += Time.deltaTime;
            timerText.text = gameTimer.ToString("N2");
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;
        gameOverUI.SetActive(true);
        endCoinText.text = "Score: " + ScoreManager.Instance.TotalScore();
        endTimerText.text = gameTimer.ToString("N2");
    }

    public void UpdateCoinUI()
    {
        coinText.text = ScoreManager.Instance.coins.ToString();
        PlayPulse(coinText.transform);
    }

    public void PlayPulse(Transform target)
    {
        target.DOKill(); // Stop any ongoing tweens
        target.localScale = Vector3.one; // Reset scale

        target.DOScale(1.2f, 0.15f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                target.DOScale(1f, 0.15f).SetEase(Ease.InBack);
            });
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCoinsCollected -= UpdateCoinUI;
        GameManager.Instance.OnGameOver -= GameOver;
    }
}
