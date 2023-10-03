using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
public class GameEndUI : MonoBehaviour
{
    public Button btnHome;
    public Button btnRestart;
    public TMPro.TMP_Text scoreText;
    [SerializeField]List<GameObject> obj = new List<GameObject>();    
    // Start is called before the first frame update
    public void Init(GameForm gameForm)
    {
        scoreText.text = "Score : " + gameForm.score+ " / "+ gameForm.TotalQuestion;
        float starValue =gameForm.score/(gameForm.TotalQuestion/3) ;
        int roundedUpValue = (int)Mathf.Ceil(starValue);
        //Debug.Log();
        for (int i = 0; i < obj.Count; i++)
        {
            if(roundedUpValue > i)
            {
                obj[i].SetActive(true);
            }else
            {
                obj[i].SetActive(false);
            }
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
