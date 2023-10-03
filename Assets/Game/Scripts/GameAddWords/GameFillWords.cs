using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FillWordsData
{
    public Sprite icon;
    public List<char> Question = new List<char>();
    public List<char> Answer = new List<char>();
    public List<char> Choice = new List<char>();
}
public class GameFillWords : GameForm
{
    public static GameFillWords Instance;
    public GameFillWordsUI gameFillWords;
    public List<String> words = new List<String>();
    public List<FillWordsData> Question = new List<FillWordsData>();
    private List<Sprite> newQuestion = new List<Sprite>();
    public int currentQuestion;

public TMPro.TMP_Text headText;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameFillWords = GetComponent<GameFillWordsUI>();
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

    public void SetLevel()
    {
        headText.text = (currentQuestion+1) + " / " + TotalQuestion;
        gameFillWords.SetUI(Question[currentQuestion]);
        panel.SetActive(false);
    }

    private void GenerateQuestion()
    {
        int randomIndex = UnityEngine.Random.Range(0, newQuestion.Count);
        FillWordsData newData = new FillWordsData();
        newData.icon = newQuestion[randomIndex];
        List<char> nameChars = ConvertToList(newQuestion[randomIndex].name);
        List<char> Choice = new List<char>();

        // Check if there are more than 5 characters in the name
        if (nameChars.Count > 5)
        {
            List<int> indicesToReplace = new List<int>();
            // Find the indices of characters to be replaced with "_"
            for (int i = 0; i < nameChars.Count; i++)
            {
                if (nameChars[i] != ' ')
                {
                    indicesToReplace.Add(i);
                }
            }

            // Randomly select 2 indices to replace with "_"
            for (int i = 0; i < Mathf.Min(2, indicesToReplace.Count); i++)
            {
                int randomCharIndex = UnityEngine.Random.Range(0, indicesToReplace.Count);
                int charIndexToReplace = indicesToReplace[randomCharIndex];
                Choice.Add(nameChars[charIndexToReplace]);
                nameChars[charIndexToReplace] = '_';
                indicesToReplace.RemoveAt(randomCharIndex);
            }
        }
        else if (nameChars.Count > 0)
        {
            // If there are less than 5 characters, randomly replace one character with "_"
            int randomCharIndex = UnityEngine.Random.Range(0, nameChars.Count);
            Choice.Add(nameChars[randomCharIndex]);
            nameChars[randomCharIndex] = '_';
        }

        // Add random characters from A-Z to Choice to make it 4 characters
        while (Choice.Count < 4)
        {
            char randomChar = (char)('A' + UnityEngine.Random.Range(0, 26));
            if (!Choice.Contains(randomChar))
            {
                Choice.Add(randomChar);
            }
        }

        // Shuffle the Choice list
        ShuffleList(Choice);

        newData.Choice = Choice;
        newData.Question = nameChars;
        newData.Answer = ConvertToList(newQuestion[randomIndex].name);
        Question.Add(newData);
        newQuestion.Remove(newQuestion[randomIndex]);
    }

    // Function to shuffle a list
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, n);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public List<char> ConvertToList(string input)
    {
        List<char> words = new List<char>();
        foreach (char c in input)
        {
            words.Add(c);
        }
        return words;
    }
    public char RandomChoice(string value)
    {
        while (true)
        {
            char c = (char)('A' + UnityEngine.Random.Range(0, 26));
            if (c.ToString() != value)
                return c;
        }
    }
    public void CheckWin()
    {

        List<char> words = new List<char>();
        foreach (Transform obj in gameFillWords.posAddWords.transform)
        {
            string s = obj.GetComponentInChildren<TMPro.TMP_Text>().text;
            words.Add(s[0]);
        }
        foreach (char c in words)
        {
            if (c == '_')
                //{

                return;
            //}
        }
        panel.SetActive(true);
        List<int> IncorrectIndex = new List<int>();
        bool areEqual = words.SequenceEqual(Question[currentQuestion].Answer);
        if (areEqual)
        {
            Debug.Log("You win");
            score++;
        }
        else
        {
            Debug.Log("You lose");

            // หาคำที่ไม่ตรงกันและแสดงผล
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

        }
        gameFillWords.SetEndLevel(IncorrectIndex);
        StartCoroutine(SetNextLevel());

    }

    IEnumerator SetNextLevel()
    {
        yield return new WaitForSeconds(waitNextLevel);
        SetLevel();
    }
}