using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource menuBgm;
    public AudioSource gameBgm;
    public AudioSource coinCollectEffect;
    public AudioSource gameoverEffect;

    private void OnEnable()
    {
        GameManager.Instance.OnCoinsCollected += CoinEffect;
        GameManager.Instance.OnGameOver += GameEnd;
        GameManager.Instance.OnMenu += InMenu;
        GameManager.Instance.OnStartGame += GameStart;
    }

    private void InMenu()
    {
        menuBgm.Play();
        gameBgm.Stop();
    }

    private void GameStart()
    {
        menuBgm.Stop();
        gameBgm.Play();
    }

    private void GameEnd()
    {
        menuBgm.Stop();
        gameBgm.Stop();
        gameoverEffect.Play();
    }

    private void CoinEffect()
    {
        coinCollectEffect.Play();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCoinsCollected -= CoinEffect;
        GameManager.Instance.OnGameOver -= GameEnd;
        GameManager.Instance.OnMenu -= InMenu;
        GameManager.Instance.OnStartGame -= GameStart;
    }
}
