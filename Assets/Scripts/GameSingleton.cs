using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton instance;


    public readonly int scorePerCar=50;
    public readonly float levelGoal=300;
    public UserInterface _interface;
    public List<CarGenerator> _carGenerator;
    public SoundEffectHandler _soundEffectHandler;
    
    public bool IsGameEnded { get; private set; }
    
    public bool GameBegins { get; private set; }
    
    public float Score { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        ResetOnAwake();
    }

    private void ResetOnAwake()
    {
        _interface.UpdateScore();
        _interface.Reset();
    }


    public void Win()
    {
        IsGameEnded = true;
        GameBegins = false;
    }

    public void Gameover()
    {
        IsGameEnded = true;
        GameBegins = false;
    }

    public void BeginGame()
    {
        GameBegins = true;
        IsGameEnded = false;
        Score = 0f;
        _interface.UpdateScore();
        foreach (var c in _carGenerator)
        {
            c.StartGameLoop();
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        Score = Mathf.Clamp(Score, 0, levelGoal);
        _interface.UpdateScore();

        print(Score);

        if (Score == levelGoal)
        {
            _interface.ShowEndMenu();
            Win();
        }
    }
}
