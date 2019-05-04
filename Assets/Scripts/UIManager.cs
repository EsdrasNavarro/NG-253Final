using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instace;

    public static UIManager Instance
    {
        get
        {
            if (_instace == null)
            {
                Debug.LogError("GameManager os null");
            }
            return _instace;
        }
    }

    [SerializeField]
    private GameObject _playerScoreTxt;

    [SerializeField]
    private GameObject pnlGameOver;

    [SerializeField]
    private Slider enemyBossLife;

    [SerializeField]
    private GameObject pnlBossLive;

    [SerializeField]
    private GameObject pnlDisplay;

    [SerializeField]
    private TextMeshProUGUI txtFinalScore;

    [SerializeField]
    private Image[] _playerLives;


    private void Awake()
    {
        _instace = this;
}


    public void UpdatePlayerScore(int score)
    {
        _playerScoreTxt.GetComponent<TextMeshProUGUI>().text = "" + score;
        
    }

    public void UpdatePlayerLives(int lives)
    {
       
        Debug.Log("Lives ->" + lives);
        for (int i = 0; i <= lives; i++)
        {
            if (i == lives)
            {
                _playerLives[i].enabled = false;
            }
        }
    }

    public void Play()
    {
       SceneManager.LoadScene("History", LoadSceneMode.Single);   
    }
    
    public void LoadLevel(int level)
    {
        GameManager.Instance.NextLevel(level);
        pnlBossLive.SetActive(false);
        pnlGameOver.SetActive(false);
        pnlDisplay.SetActive(false);
        SceneManager.LoadScene(level);
        
    }


    private IEnumerator LoadSceneRountine(int level)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }


   
    public void CloseGame()
    {
        Application.Quit();
    }


    private void ResetLivesUI()
    {
        foreach (Image live in _playerLives)
        {
            live.enabled = true;
        }
    }

    public void UpdateEnemyBossLife()
    {
        if(enemyBossLife.value <= 0)
        {
            // destruiu o boss
            StartCoroutine(GameWinRoutine());
            // Ganhamos
        }
        enemyBossLife.value -= 2;
        Debug.Log("VALUEEE" + enemyBossLife.value);
    }


    private IEnumerator GameWinRoutine()
    {
        yield return new WaitForSeconds(1f);
        pnlDisplay.SetActive(true);
        txtFinalScore.text = "Score :" + _playerScoreTxt.GetComponent<TextMeshProUGUI>().text;
        Time.timeScale = 0;
    }

    public void ShowEnemyBossLife()
    {
        pnlBossLive.SetActive(true); 
    }

    public void GameOver()
    {
        pnlGameOver.SetActive(true);
        Time.timeScale = 0;
    }



}
