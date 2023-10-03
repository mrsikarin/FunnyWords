using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//public 

public class RankingUI : MonoBehaviour
{
    public Button button;
    public GameObject itemPrefabs;
    public Transform pos;
    public List<ModeScore> ttt = new List<ModeScore>();
    public void CreateUI(string modeName)
    {
        Debug.Log("Name = "+modeName);
        ttt = new List<ModeScore>(SaveManager.Instance.ModeScores);
        List<ModeScore> newModeScore = SaveManager.Instance.ModeScores
            .Where(item => item.modeName == modeName)
            .OrderByDescending(item => item.totalScore)
            .ToList(); int i = 0;
        foreach (var item in newModeScore)
        {
            i++;
            Debug.Log(i);
            RankingItemUI rankingItemUI = Instantiate(itemPrefabs, pos).GetComponent<RankingItemUI>();
            rankingItemUI.SetUI(item, i);
        }


    }

}
