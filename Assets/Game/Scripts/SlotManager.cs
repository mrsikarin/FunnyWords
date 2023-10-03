using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;
    public List<String> words = new List<String>();
    public List<DragItem> words1 = new List<DragItem>();
    public Dictionary<GameObject, String> GameObjectByWords = new Dictionary<GameObject, String>();
    public GameObject ItemDrag;
    public Transform pos;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (var item in words)
        {
           DragItem dragItem = Instantiate(ItemDrag,pos).GetComponentInChildren<DragItem>();
           dragItem.SetupUI(item);
           words1.Add(dragItem);
           GameObjectByWords.Add(dragItem.gameObject,item);
        
        }
    }
    public void SwapItems(GameObject item1, GameObject item2)
    {

    }
    public void ChickWin()
    {
        List<String> words = new List<String>();
        foreach (Transform child in pos.transform)
        {
            words.Add(child.GetComponentInChildren<DragItem>().wordsAssign);
            //childObjects.Add(child.gameObject);
        }
        bool areEqual = words.SequenceEqual(this.words);
        if(areEqual)
        {
            Debug.Log("You win");
        }

    }
}