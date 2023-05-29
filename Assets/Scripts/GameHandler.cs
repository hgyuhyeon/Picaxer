﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    private static GameHandler instance;

    [SerializeField] private Snake snake;

    //private static int score;

    private LevelGrid levelGrid;

    private void Awake()
    {
        instance = this;
        Score.InitializeStatic();

        //PlayerPrefs.SetInt("highscore",100);
        //PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetInt("highscore"));
        Score.TrySetNewHighscore(200);
    }

    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        //GameObject snakeHeadGameObject = new GameObject();
        //SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        //snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite;

        //CMDebug.ButtonUI(Vector2.zero, "Reload Scene", () =>
        //{
        //    Loader.Load(Loader.Scene.GameScene);
        //});
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused())
            {
                GameHandler.ResumeGame();
            }
            else
            {
                GameHandler.PauseGame();
            }
        }
    }
    /*
    private static void InitializeStatic()
    {
        score = 0;
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 100;
    }
    */

    public static void SnakeDied()
    {
        bool isNewHighScore = Score.TrySetNewHighscore();
        GameOverWindow.ShowStatic(isNewHighScore);
        ScoreWindow.HideStatic();
    }

    public static void ResumeGame()
    {
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public static void PauseGame()
    {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }

}
