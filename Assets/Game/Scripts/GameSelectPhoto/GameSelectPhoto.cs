using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectPhotoData
{
    public string Question;
    public List<Sprite> choice = new List<Sprite>();
    public int answer;
    public bool CheckAnswer(int answer)
    {
        if(this.answer == answer)
            return true;
        return false;
    }
    public void RandomChoice()
    {
        int randomChoice = Random.Range(0, choice.Count);
        Sprite tempChoice = choice[randomChoice];
        choice[randomChoice] = choice[0];
        choice[0] = tempChoice;
        answer = randomChoice;
        Question = choice[answer].name;
    }
}
public class GameSelectPhoto : GameForm
{
    public static GameSelectPhoto Instance;
    public GameSelectPhotoUI selectPhotoUI;
    public List<SelectPhotoData> Question = new List<SelectPhotoData>();
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
        List<Sprite> choice = new List<Sprite>();
        choice.Add(newQuestion[randomIndex]);
        while(choice.Count < choiceCount)
        {
            int choiceIndex = Random.Range(0, gameData.listPhotos.Count);
            if(!choice.Contains(gameData.listPhotos[choiceIndex]))
            {
                choice.Add(gameData.listPhotos[choiceIndex]);
            }
        }

        SelectPhotoData selectPhotoData = new SelectPhotoData();
        selectPhotoData.choice = choice;
        selectPhotoData.RandomChoice();
        newQuestion.Remove(newQuestion[randomIndex]);
        Question.Add(selectPhotoData);
        
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
            //GameManager.Instance.OnLevelComplete();
           // Instantiate(GameManager.Instance.GameEndUI,pos);
        }
        StartCoroutine(SetNextLevel());
    }
    public void CreateQuestion()
    {
        headText.text = (currentQuestion+1) + " / " + TotalQuestion;
        selectPhotoUI.SetUI(Question[currentQuestion]);
        panel.SetActive(false);
    }
    IEnumerator SetNextLevel()
    {
        yield return new WaitForSeconds(waitNextLevel);
        CreateQuestion();
    }

}