using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int coins = 0;
    public float timeSurvived = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCoinsCollected += AddCoin;
    }

    private void Update()
    {
        timeSurvived += Time.deltaTime;
    }

    public void AddCoin()
    {
        coins++;
    }

    public int TotalScore() => coins;

    private void OnDisable()
    {
        GameManager.Instance.OnCoinsCollected -= AddCoin;
    }
}
