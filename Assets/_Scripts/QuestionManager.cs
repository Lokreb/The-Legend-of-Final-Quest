using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Question
{
    public string questionType;
    public string questionText;
    public List<string> choices;
    public int minValue;
    public int maxValue;
    
}

[System.Serializable]
public class QuestionData
{
    public List<Question> questions;
}

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public GameObject questionPanel;
    public int NbQuestion;
    private QuestionData questionData;
    private int currentQuestionIndex;
    public bool Repondu = false;
    void Start()
    {
        LoadQuestionsFromJSON(); // Charger les questions depuis le fichier JSON
        currentQuestionIndex = 0;
        NbQuestion = questionData.questions.Count;
        
        DisplayQuestion(currentQuestionIndex); // Afficher la première question
    }

    void LoadQuestionsFromJSON()
    {
        string jsonPath = "Assets/StreamingAssets/questionnaire.json"; // Chemin vers le fichier JSON
        string json = File.ReadAllText(jsonPath);

        questionData = JsonUtility.FromJson<QuestionData>(json);
    }

    void DisplayQuestion(int questionIndex)
    {
        if (questionIndex < questionData.questions.Count)
        {
            Question currentQuestion = questionData.questions[questionIndex];

            questionText.text = currentQuestion.questionText;

            if (currentQuestion.questionType == "choice" || currentQuestion.questionType == "multiple-choice")
            {
                for (int i = 0; i < answerTexts.Length; i++)
                {
                    if (i < currentQuestion.choices.Count)
                    {
                        answerTexts[i].text = currentQuestion.choices[i];
                        answerTexts[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        answerTexts[i].gameObject.SetActive(false);
                    }
                }
            }
            else if (currentQuestion.questionType == "scale")
            {
                // Afficher une échelle, par exemple, de 1 à 10
                for (int i = 0; i < answerTexts.Length; i++)
                {
                    answerTexts[i].text = (currentQuestion.minValue + i).ToString();
                    answerTexts[i].gameObject.SetActive(true);
                }
            }
            else
            {
                // Cacher les réponses si c'est une question à réponse libre
                for (int i = 0; i < answerTexts.Length; i++)
                {
                    answerTexts[i].gameObject.SetActive(false);
                }
            }

            questionPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Toutes les questions ont été posées.");
            questionPanel.SetActive(false);
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        Repondu = true;
        DisplayQuestion(currentQuestionIndex);
    }
}