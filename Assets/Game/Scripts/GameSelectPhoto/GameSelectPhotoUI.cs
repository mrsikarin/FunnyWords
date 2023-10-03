using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameSelectPhotoUI : MonoBehaviour
{
    public List<Button> btnForSelection = new List<Button>();
    public Color Correct;
    public Color Incorrect;
    public TMP_Text nameText;
    private SelectPhotoData selectPhotoData;
    public RectTransform Symbol;
    public Vector2 newPos;
    private Vector2 originalPosition;
    void Start()
    {
        originalPosition = Symbol.anchoredPosition;
        for (int i = 0; i < btnForSelection.Count; i++)
        {
            int index = i; // Capture the current value of i
            btnForSelection[i].onClick.AddListener(delegate { AssignBtn(index); });
        }
    }
    public void SetUI(SelectPhotoData data)
    {
        LeanTween.move(Symbol, new Vector3(originalPosition.x,originalPosition.y , 0f), 0.5f)
        .setEase(LeanTweenType.easeOutQuad); 
        nameText.text = data.Question;
        selectPhotoData = data;
       // icon.sprite = data.icon;
        for (int i = 0; i < data.choice.Count; i++)
        {
            //int index = i; // Capture the current value of i

            // Example: Set button text based on index (you can modify this logic as needed)
            //btnForSelection[i].transform.GetComponentInChildren<Image>().sprite = data.choice[i];
            btnForSelection[i].GetComponent<Image>().color = Color.white;
            btnForSelection[i].transform.GetChild(0).GetComponent<Image>().sprite = data.choice[i];

          //  btnForSelection[i].onClick.AddListener(delegate { AssignBtn(index); });
        }
    }
    public void AssignBtn(int index)
    {
       // Debug.Log(index);
        GameSelectPhoto.Instance.CheckChoice(index);
        SetEndLevel(index);
    }
    public void SetEndLevel(int index)
    {
        //Symbol.
        //for (int i = 0; i < Symbol.transform.childCount; i++)
       // {
        if(index == selectPhotoData.answer){
            Symbol.transform.GetChild(0).gameObject.SetActive(true);
            Symbol.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Symbol.transform.GetChild(0).gameObject.SetActive(false);
            Symbol.transform.GetChild(1).gameObject.SetActive(true);
        }
        //}
        LeanTween.move(Symbol, new Vector3(newPos.x,newPos.y , 0f), 1f)
        .setEase(LeanTweenType.easeOutQuad); 
        btnForSelection[index].GetComponent<Image>().color = Incorrect;
        btnForSelection[selectPhotoData.answer].GetComponent<Image>().color = Correct;
    }
}
