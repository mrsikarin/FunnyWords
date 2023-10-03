using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSortWordsUI : MonoBehaviour
{
    public Image icon;
    public GameObject prefabs1;
    public GameObject prefabs2;
    public Transform posAddWords;
    public Transform posSelectWords;
    public Transform posAnswer;
    public RectTransform Symbol;
    public Vector2 newPos;
    private Vector2 originalPosition;
    SortWordsData sortWordsData;
    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = Symbol.anchoredPosition;
    }

    public void SetUI(SortWordsData Data)
    {
                LeanTween.move(Symbol, new Vector3(originalPosition.x,originalPosition.y , 0f), 0.5f)
        .setEase(LeanTweenType.easeOutQuad);
        sortWordsData = Data;
        icon.sprite = sortWordsData.icon;
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
        foreach (var Word in sortWordsData.Answer)
        {
            Instantiate(prefabs1, posAddWords);
        }
        foreach (var Word in sortWordsData.Question)
        {
            GameObject btn =  Instantiate(prefabs2, posSelectWords);
            btn.GetComponent<SelectWordBtn>().Words = Word[0];
            btn.GetComponent<SelectWordBtn>().SetUI();
        }

    }

    public void SetAnswer()
    {
        for (int i = 0;i< GameSortWords.Instance.words.Count;i++)
        {
            posAddWords.GetChild(i).GetComponentInChildren<TMPro.TMP_Text>().text = GameSortWords.Instance.words[i];
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

        foreach (var Word in sortWordsData.Answer)
        {
            GameObject obj = Instantiate(prefabs1, posAnswer);
            obj.GetComponentInChildren<TMPro.TMP_Text>().text = Word.ToString();
            obj.GetComponent<Image>().color = GameManager.Instance.Correct;
            obj.transform.GetComponent<Image>().enabled = false;
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }
        /*for (int i = 0; i < sortWordsData.Question.Count; i++)
        {
            if(sortWordsData.Question[i] == "_" )
            {
                posAddWords.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Correct;
                posAnswer.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Correct;
            }else
            {
                posAnswer.transform.GetChild(i).GetComponent<Image>().enabled = false;
                posAnswer.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        } */
        foreach (int i in index)
        {
            posAnswer.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            posAnswer.transform.GetChild(i).GetComponent<Image>().enabled = true;
            posAddWords.transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Incorrect;
        } 
     
    }
}
