using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int GAME_STATUS_NOT_STARTED = 0;
    public const int GAME_STATUS_PLAYING = 1;
    public const int GAME_STATUS_PAUSED =  2;
    public const float LEVEL_TIME = 30f; 


    public float levelTime = 0f; 
    public int gameStatus; // não iniciado
    public bool isBoss = false;
   

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No Game Manager");
            }
            return _instance;
        }
    }

    public Player Player { get; private set; }
    public int Level { get; set; }

    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameStatus = GameManager.GAME_STATUS_PLAYING;
    }

    private void FixedUpdate()
    {
        if (gameStatus == GAME_STATUS_PLAYING)
        {
            levelTime = Time.timeSinceLevelLoad;
            
            if (levelTime >= LEVEL_TIME) // tempo para o boss
            {
                isBoss = true;
                UIManager.Instance.ShowEnemyBossLife();
               
            }
        }
    }


    public void NextLevel(int level)
    {
        Time.timeScale = 1;
        gameStatus = GameManager.GAME_STATUS_PLAYING;
        levelTime = 0;
        Level = level++;
        isBoss = false;
    }

}
