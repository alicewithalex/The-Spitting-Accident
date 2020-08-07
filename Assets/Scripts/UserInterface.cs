using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, inGameMenu, endMenu;
    [SerializeField] private GameObject winScreen, loseScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        inGameMenu.SetActive(true);
        mainMenu.SetActive(false);
        endMenu.SetActive(false);
        GameSingleton.instance.BeginGame();
    }

    public void RepeatButtonPressed()
    {
        inGameMenu.SetActive(true);
        mainMenu.SetActive(false);
        endMenu.SetActive(false);
        GameSingleton.instance.BeginGame();
    }

    public void MenuButtonPressed()
    {
        inGameMenu.SetActive(false);
        mainMenu.SetActive(true);
        endMenu.SetActive(false);
    }

    public void Reset()
    {
        inGameMenu.SetActive(false);
        mainMenu.SetActive(true);
        endMenu.SetActive(false);
    }
    
    public void UpdateScore()
    {
        scoreText.text = $"{GameSingleton.instance.Score}/{GameSingleton.instance.levelGoal}";
    }
    
    public void ShowEndMenu()
    {
        inGameMenu.SetActive(false);
        mainMenu.SetActive(false);
        endMenu.SetActive(true);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
    }
}
