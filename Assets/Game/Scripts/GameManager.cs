using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameData> gameDatas = new List<GameData>();
    public List<ModeData> modeDatas = new List<ModeData>();
    public GameData currentGameData;
    public ModeData currentModeData;
    public GameObject GameEndUI;
    public GameObject PauseUI;
    public Color Correct;
    public Color Incorrect;
    string playerName;
    string modeName;
    string levelName;
    //public SaveManager saveManager;
    public string PlayerName { get { return playerName; } set { playerName = value; } }
    public string ModeName { get { return modeName; } set{ modeName = value; } }
    public string LevelName { get{ return levelName;} set{ levelName = value;} }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SaveManager.Instance = GetComponent<SaveManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnLevelComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
