using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager instance;
    public GameObject blockManager;
    public Transform ball, player;
    public CountDown countDownWindow;
    public WinWindow winWindow;
    public GameOverWindow gameOverWindow;

    [SerializeField] private GamePlayState gameplaState;
    public Action<GamePlayState> OnGamePlayState;

    public void Awake()
    {
        if (GamePlayManager.instance == null) GamePlayManager.instance = this;
        else if(GamePlayManager.instance != this) Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        Awake();

        GamePlayState = GamePlayState.Ready;

        blockManager.SetActive(true);
        ball.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }

    public GamePlayState GamePlayState { 
        get { 
            return gameplaState; 
        }

        set { 
            gameplaState = value;
            if (OnGamePlayState != null) OnGamePlayState(gameplaState);

            ResetWindows();
            switch (GamePlayState)
            {
                case GamePlayState.Ready:
                    countDownWindow.gameObject.SetActive(true);
                    break;
                case GamePlayState.Won:
                    winWindow.gameObject.SetActive(true);
                    break;
                case GamePlayState.GameOver:
                    gameOverWindow.gameObject.SetActive(true);
                    break;
            }
        }    
    }

    public void ResetWindows()
    {
        if (countDownWindow != null) countDownWindow.gameObject.SetActive(false);
        if (winWindow != null) winWindow.gameObject.SetActive(false);
        if (gameOverWindow != null) gameOverWindow.gameObject.SetActive(false);
    }

}

public enum GamePlayState
{
    Ready,
    Playing,
    GameOver,
    Won
}
