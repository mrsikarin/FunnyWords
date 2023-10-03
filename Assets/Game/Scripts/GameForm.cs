using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameForm : MonoBehaviour
{
    
    public GameData gameData;
    public GameObject panel;
    public int score = 0;
    public Transform posComplete;
    public int TotalQuestion;
    public float waitNextLevel = 5f;
    public void OnLevelComplete()
    {
        Instantiate(GameManager.Instance.GameEndUI,posComplete).GetComponentInChildren<GameEndUI>().Init(this);
        SaveManager.Instance.SaveScore(GameManager.Instance.PlayerName,GameManager.Instance.currentModeData.nameData,GameManager.Instance.LevelName,score);
        
    }
    public void PauseGame()
    {
        Instantiate(GameManager.Instance.PauseUI,posComplete);
    }


}
