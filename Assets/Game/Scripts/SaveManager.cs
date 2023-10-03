using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class ModeScore
{
    public string playerName;
    public string modeName;
    public int totalScore;
}
[System.Serializable]
public class PlayerLevelData
{
    public string playerName;
    public string modeName;
    public string levelName;
    public int score;
}
[System.Serializable]
public class SaveData
{
    public List<PlayerLevelData> playerLevelDataList;
    public List<ModeScore> modeScores;
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string jsonFilePath;
    private List<PlayerLevelData> playerLevelDataList = new List<PlayerLevelData>();
    
    private List<ModeScore> modeScores = new List<ModeScore>();
    public List<ModeScore> ModeScores { get { return modeScores; }}
    public SaveData saveData = new SaveData();
    void Awake()
    {
       // Instance = this;
    }
    private void Start()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "scores.json");
        LoadScores();
    }
    private void LoadScores()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            playerLevelDataList = saveData.playerLevelDataList;
            modeScores = saveData.modeScores;
        }
    }
    public void SaveScore(string playerName, string modeName,string levelName, int score)
    {
        // แก้ไขคะแนนรวมของโหมด
        //ModeScore modeScore = modeScores.Find(item => item.playerName == playerName && item.modeName == modeName);
        PlayerLevelData playerLevelData = playerLevelDataList.Find(item => item.playerName == playerName 
        && item.modeName == modeName 
        && item.levelName == levelName);

        if (playerLevelData != null)
        {
            if(playerLevelData.score < score)
                playerLevelData.score = score;
        }else
        {
            PlayerLevelData newData = new PlayerLevelData();
            newData.playerName = playerName;
            newData.modeName = modeName;
            newData.levelName = levelName;
            newData.score = score;
            playerLevelDataList.Add(newData);
        }


        List<PlayerLevelData> playerLevelDataAll = playerLevelDataList.FindAll(item => item.playerName == playerName && item.modeName == modeName);
        ModeScore modeScore = modeScores.Find(item => item.playerName == playerName && item.modeName == modeName);
        int totalScore = 0;
        foreach (var item in playerLevelDataAll)
        {
            totalScore += item.score;
        }
        if (modeScore != null)
        {
            modeScore.totalScore = totalScore;
        }
        else
        {
            modeScore = new ModeScore();
            modeScore.playerName = playerName;
            modeScore.modeName = modeName;
            modeScore.totalScore = totalScore;
            modeScores.Add(modeScore);
        }        


        SaveScores();
    }
    private void SaveScores()
    {
        var data = new SaveData();
        data.playerLevelDataList = playerLevelDataList;
        data.modeScores = modeScores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(jsonFilePath, json);
    }
}
