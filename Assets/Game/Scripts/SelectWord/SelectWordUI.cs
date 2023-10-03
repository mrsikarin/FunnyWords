using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SelectWordUI : MonoBehaviour
{
    public List<Button> btnForSelection = new List<Button>();
    public Image icon;
    public Color Correct;
    public Color Incorrect;
    public RectTransform Symbol;
    public Vector2 newPos;
    private Vector2 originalPosition;
    SelectWordData selectWordData;
    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = Symbol.anchoredPosition;
    }
    void Start()
    {
        
        for (int i = 0; i < btnForSelection.Count; i++)
        {
            int index = i; // Capture the current value of i
            btnForSelection[i].onClick.AddListener(delegate { AssignBtn(index); });
        }
    }
    public void SetUI(SelectWordData data)
    {
        LeanTween.move(Symbol, new Vector3(originalPosition.x,originalPosition.y , 0f), 0.5f)
        .setEase(LeanTweenType.easeOutQuad); 
        icon.sprite = data.icon;
        selectWordData = data;
        for (int i = 0; i < data.choice.Count; i++)
        {
           // int index = i; // Capture the current value of i
            btnForSelection[i].GetComponent<Image>().color = Color.white;
            // Example: Set button text based on index (you can modify this logic as needed)
            btnForSelection[i].GetComponentInChildren<TMP_Text>().text = data.choice[i];

           // btnForSelection[i].onClick.AddListener(delegate { AssignBtn(index); });
        }
    }
    public void AssignBtn(int index)
    {
       // Debug.Log(index);
        SelectWordManager.Instance.CheckChoice(index);
        SetEndLevel(index);
    }
    public void SetEndLevel(int index)
    {
        //Symbol.
        //for (int i = 0; i < Symbol.transform.childCount; i++)
       // {
        if(index == selectWordData.answer){
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
        btnForSelection[selectWordData.answer].GetComponent<Image>().color = Correct;
    }
}
