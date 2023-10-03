using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingItemUI : MonoBehaviour
{
    public TMPro.TMP_Text playername;
    public TMPro.TMP_Text score;
    public TMPro.TMP_Text number;
    public void SetUI(ModeScore modeScore,int index)
    {
        number.text = index.ToString();
        playername.text = modeScore.playerName;
        score.text = modeScore.totalScore.ToString();
    }

}
