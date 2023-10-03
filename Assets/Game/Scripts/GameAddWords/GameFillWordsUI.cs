using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameFillWordsUI : MonoBehaviour
{
    public Image icon;
    public GameObject prefabs1; //  ปุ่มโชว์คำ
    public GameObject prefabs2; // ปุ่มเลือกคำตอบ
    
    public Transform posAddWords;
    public Transform posSelectWords;
    public Transform posAnswer;
    public RectTransform Symbol;
    public Vector2 newPos;
    private Vector2 originalPosition;
    FillWordsData fillWordsData;
    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = Symbol.anchoredPosition;
    }
    public void SetUI(FillWordsData Data)
    {
        LeanTween.move(Symbol, new Vector3(originalPosition.x,originalPosition.y , 0f), 0.5f)
        .setEase(LeanTweenType.easeOutQuad);
         fillWordsData = Data;
        icon.sprite = fillWordsData.icon;
        
        foreach (Transform obj in posAddWords.transform)
        {
            Destroy(obj.transform.gameObject);
        }
        foreach (Transform obj in posSelectWords.transform)
        {
            Destroy(obj.transform.gameObject);
        }
        foreach (Transform obj in posAnswer.transform)
        {
            Destroy(obj.transform.gameObject);
        }

        foreach (var Word in fillWordsData.Question)
        {
            GameObject obj = Instantiate(prefabs1, posAddWords);
            obj.GetComponentInChildren<TMPro.TMP_Text>().text = Word.ToString();
        }
        foreach (var Word in fillWordsData.Choice)
        {
            GameObject btn =  Instantiate(prefabs2, posSelectWords);
            btn.GetComponentInChildren<TMPro.TMP_Text>().text = Word.ToString();
            btn.GetComponent<Button>().onClick.AddListener(delegate { onClickBtn(Word);});
        }

    }
    public void onClickBtn(char character)
    {
        
        foreach(Transform obj in posAddWords.transform)
        {
            string ch = obj.GetComponentInChildren<TMPro.TMP_Text>().text;
            if(ch == "_")
            {
                obj.GetComponentInChildren<TMPro.TMP_Text>().text = character.ToString();
                GameFillWords.Instance.CheckWin();
                return;
            }
        }
    }
    public void SetEndLevel(List<int> index)
    {
                LeanTween.move(Symbol, new Vector3(newPos.x,newPos.y , 0f), 1f)
        .setEase(LeanTweenType.easeOutQuad); 
        if(index.Count <= 0){
            Symbol.transform.GetChild(0).gameObject.SetActive(true);
            Symbol.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
        else
        {
            Symbol.transform.GetChild(0).gameObject.SetActive(false);
            Symbol.transform.GetChild(1).gameObject.SetActive(true);
        }

        foreach (var Word in fillWordsData.Answer)
        {
            GameObject obj = Instantiate(prefabs1, posAnswer);
            obj.GetComponentInChildren<TMPro.TMP_Text>().text = Word.ToString();
        }
        for (int i = 0; i < fillWordsData.Question.Count; i++)
        {
            if(fillWordsData.Question[i] == '_' )
            {
                posAddWords.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Correct;
                posAnswer.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Correct;
            }else
            {
                posAnswer.transform.GetChild(i).GetComponent<Image>().enabled = false;
                posAnswer.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        } 
        foreach (int i in index)
        {
            posAddWords.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Incorrect;
        } 
     
    }

}
