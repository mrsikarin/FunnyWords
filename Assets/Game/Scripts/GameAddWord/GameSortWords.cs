using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SortWordsData
{
    public Sprite icon;
    public List<string> Question = new List<string>();
    public List<string> Answer = new List<string>();
}
public class GameSortWords : GameForm
{
    public static GameSortWords Instance;
    public GameSortWordsUI gameSortWordsUI;
    public List<String> words = new List<String>();
    public List<SortWordsData> Question = new List<SortWordsData>();

    private List<Sprite> newQuestion = new List<Sprite>();
    public int currentQuestion;
    public TMPro.TMP_Text headText;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
            gameData = GameManager.Instance.currentGameData;
        newQuestion = new List<Sprite>(gameData.listPhotos);
        while (newQuestion.Count > 0)
        {
            GenerateQuestion();
        }
        TotalQuestion = Question.Count;
        CreateQuestion();
    }

    private void CreateQuestion()
    {
        SetLevel();
    }

    private void GenerateQuestion()
    {
        int randomIndex = UnityEngine.Random.Range(0, newQuestion.Count);
        SortWordsData sortWordsData = new SortWordsData();
        sortWordsData.icon = newQuestion[randomIndex];
        sortWordsData.Question = ConvertToList(ShuffleString(newQuestion[randomIndex].name));
        sortWordsData.Answer = ConvertToList(newQuestion[randomIndex].name);
        Question.Add(sortWordsData);
        newQuestion.Remove(newQuestion[randomIndex]);
    }

    private string ShuffleString(string input)
    {
        List<char> charList = new List<char>();

        foreach (char c in input)
        {
            charList.Add(c);
        }

        List<char> shuffledCharList = new List<char>();

        while (charList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, charList.Count);
            char randomChar = charList[randomIndex];
            shuffledCharList.Add(randomChar);
            charList.RemoveAt(randomIndex);
        }

        string shuffledString = new string(shuffledCharList.ToArray());

        Debug.Log("Original: " + input);
        Debug.Log("Shuffled: " + shuffledString);

        return shuffledString;

    }
    public List<string> ConvertToList(string input)
    {
        List<string> words = new List<string>();
        foreach (char c in input)
        {
            words.Add(c.ToString());
        }
        return words;
    }
    public void ChickWin(char word)
    {

        words.Add(word.ToString());
        gameSortWordsUI.SetAnswer();
        List<int> IncorrectIndex = new List<int>();
        if (words.Count == Question[currentQuestion].Question.Count)
        {
            panel.SetActive(true);
            bool areEqual = words.SequenceEqual(Question[currentQuestion].Answer);
            if (areEqual)
            {
                Debug.Log("You win");
                score++;
            }
            else
            {
                Debug.Log("No win");
                for (int i = 0; i < words.Count; i++)
                {
                    if (words[i] != Question[currentQuestion].Answer[i])
                    {
                        IncorrectIndex.Add(i);
                    }
                }
            }

            currentQuestion++;
            if (currentQuestion > Question.Count - 1)
            {
                OnLevelComplete();
                return;
                //GameManager.Instance.OnLevelComplete();
                // Instantiate(GameManager.Instance.GameEndUI,pos);
            }
            gameSortWordsUI.SetEndLevel(IncorrectIndex);
            StartCoroutine(SetNextLevel());

        }

    }
    public void ResetLevel()
    {
        SetLevel();
    }
    public void SetLevel()
    {
        headText.text = (currentQuestion+1) + " / " + TotalQuestion;
        panel.SetActive(false);
        words = new List<String>();
        gameSortWordsUI.SetUI(Question[currentQuestion]);
    }
    IEnumerator SetNextLevel()
    {
        yield return new WaitForSeconds(waitNextLevel);
        SetLevel();
    }
}
