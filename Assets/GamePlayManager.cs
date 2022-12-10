using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public static GamePlayManager instance;
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

    private void OnEnable() => GamePlayState = GamePlayState.Ready;

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
        countDownWindow.gameObject.SetActive(false);
        winWindow.gameObject.SetActive(false);
        gameOverWindow.gameObject.SetActive(false);
    }

}

public enum GamePlayState
{
    Ready,
    Playing,
    GameOver,
    Won
}
