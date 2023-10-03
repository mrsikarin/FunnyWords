using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class SelectWordData
{
    public Sprite icon;
    public List<string> choice = new List<string>();
    public int answer;
    public SelectWordData(Sprite icon, List<string> choice, int answer)
    {
        this.icon = icon;
        this.choice = new List<string>(choice);
        this.answer = answer;
        RandomChoice();
    }
    public bool CheckAnswer(int answer)
    {
        if(this.answer == answer)
            return true;
        return false;
    }
    public void RandomChoice()
    {
        int randomChoice = Random.Range(0, choice.Count);
        string tempChoice = choice[randomChoice];
        choice[randomChoice] = choice[0];
        choice[0] = tempChoice;
        answer = randomChoice;
    }
}


public class SelectWordManager : GameForm
{
    public static SelectWordManager Instance;
    public SelectWordUI selectWordUI;
    
    public List<SelectWordData> Question = new List<SelectWordData>();
    private List<Sprite> newQuestion = new List<Sprite>();
    public TMPro.TMP_Text headText;
    public int currentQuestion = 0;
    public int choiceCount = 4;
    void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        if(GameManager.Instance != null)
            gameData = GameManager.Instance.currentGameData;
        newQuestion = new List<Sprite>(gameData.listPhotos);
        while(newQuestion.Count > 0)
        {
            GenerateQuestion();
        }
        TotalQuestion =  Question.Count;
        CreateQuestion();
    }
    public void GenerateQuestion()
    {
        int randomIndex = Random.Range(0, newQuestion.Count);
        int correctAnswer = randomIndex;
        List<string> choice = new List<string>();
        choice.Add(newQuestion[randomIndex].name);
        while(choice.Count < choiceCount)
        {
            int choiceIndex = Random.Range(0, gameData.listPhotos.Count);
            if(!choice.Contains(gameData.listPhotos[choiceIndex].name))
            {
                choice.Add(gameData.listPhotos[choiceIndex].name);
            }
        }

        SelectWordData selectWordData = new SelectWordData(newQuestion[randomIndex],choice,0);
        newQuestion.Remove(newQuestion[randomIndex]);
        Question.Add(selectWordData);
        
    }

    public void CheckChoice(int index)
    {
        panel.SetActive(true);
        if(Question[currentQuestion].CheckAnswer(index))
            score++;
            
        currentQuestion++;
        if(currentQuestion > Question.Count-1)
        {
            OnLevelComplete();
            return;
        }        
        StartCoroutine(SetNextLevel());
        
    }
    public void CreateQuestion()
    {
        headText.text = (currentQuestion+1) + " / " + TotalQuestion;
        selectWordUI.SetUI(Question[currentQuestion]);
        panel.SetActive(false);
    }
    IEnumerator SetNextLevel()
    {
        yield return new WaitForSeconds(waitNextLevel);
        CreateQuestion();
    }

}
