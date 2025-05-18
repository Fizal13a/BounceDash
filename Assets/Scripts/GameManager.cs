using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform splash;

    public UnityAction OnCoinsCollected;
    public UnityAction OnGameOver;
    public UnityAction OnMenu;
    public UnityAction OnStartGame;

    private void Awake()
    {
        //Setting up singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Time.timeScale = 1;
    }

    private void Start()
    {
        OnMenu.Invoke();
    }

    public void Splash()
    {
        StartCoroutine(PlaySplash());
    }

    IEnumerator PlaySplash()
    {
        // in
        splash.DOMoveX(5, 1f).SetEase(Ease.InOutFlash);
        yield return new WaitForSeconds(1.5f);

        // out
        splash.DOMoveX(12, 1f).SetEase(Ease.OutFlash);
        yield return new WaitForSeconds(1.2f);

        // Reset position
        splash.transform.position = new Vector3(-2.5f, 0, 0);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
        OnStartGame.Invoke();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        OnMenu.Invoke();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
