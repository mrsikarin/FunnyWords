using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public TMPro.TMP_Text playerNameText;
    void Start()
    {
        playerNameText.text = "Name : " + GameManager.Instance.PlayerName;
    }
    public void LoadScene(int index)
    {
         GameManager.Instance.currentModeData = GameManager.Instance.modeDatas[index];
        SceneManager.LoadScene("LevelSelects");
       
    }
    public void Logout()
    {
        SceneManager.LoadScene("InputName");
        //GameManager.Instance.ModeName = "Mode";
    }
    public void LoadSceneRanking()
    {
        SceneManager.LoadScene("Ranking");
       
    }   

}
