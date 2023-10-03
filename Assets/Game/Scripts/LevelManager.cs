using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public GameObject btnPrefabs;
    [SerializeField]Transform positionBtn;
    public string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        foreach (var item in GameManager.Instance.gameDatas)
        {
           Button btn = Instantiate(btnPrefabs,positionBtn.transform).GetComponent<Button>();
           btn.GetComponentInChildren<TMP_Text>().text = item.nameData;
           GameData gameData = item;
           btn.onClick.AddListener(() => SelectLevel(gameData));
        }
    }
    public void SelectLevel(GameData gameData)
    {
        GameManager.Instance.currentGameData = gameData;
        GameManager.Instance.LevelName = gameData.nameData; 
        SceneManager.LoadScene(GameManager.Instance.currentModeData.SceneName);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
