using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public GameObject btnMode;
    public GameObject itemPrefabs;
    public Transform pos;
    public Transform posBtn;
    public Dictionary<string, GameObject> ListTabs = new Dictionary<string, GameObject>();
    void Start()
    {
        foreach (var data in GameManager.Instance.modeDatas)
        {
            Button button = Instantiate(btnMode, posBtn).GetComponent<Button>();
            button.onClick.AddListener(delegate { OnClickTabRanking(data.nameData); });
            button.GetComponentInChildren<TMPro.TMP_Text>().text = data.nameData;

            RankingUI pageRankingUI = Instantiate(itemPrefabs, pos).GetComponent<RankingUI>();
            pageRankingUI.CreateUI(data.nameData);
            Debug.Log(data.nameData);
            pageRankingUI.gameObject.SetActive(false);

            ListTabs.Add(data.nameData, pageRankingUI.gameObject);

            // เช็คว่า data คือรายการแรกในลูป
            if (data == GameManager.Instance.modeDatas[0])
            {
                pageRankingUI.gameObject.SetActive(true);
            }
        }
    }
    public void OnClickTabRanking(string modeName)
    {
        foreach (KeyValuePair<string, GameObject> item in ListTabs)
        {
            if (modeName == item.Key)
            {
                item.Value.SetActive(true);
            }
            else
            {
                item.Value.SetActive(false);
            }
        }
    }
}
