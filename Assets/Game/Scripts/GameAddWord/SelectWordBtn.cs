using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectWordBtn : MonoBehaviour
{
    public TMPro.TMP_Text textWords;
    private char words;
    public char Words { set { words = value; } get { return words; } }
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }
    public void SetUI()
    {
        textWords.text = words.ToString();
    }
    // Update is called once per frame

    public void ClickBtn()
    {
      GameSortWords.Instance.ChickWin(words);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
