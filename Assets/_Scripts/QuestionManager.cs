using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Parties
{
    public QuestionPartie[] parties;
    
}
[System.Serializable]
public class QuestionPartie
{
    public int partie;
    public string partieNom;
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public string questionType;
    public string questionText;
    public string[] choices;
    public int minValue;
    public int maxValue;
    
}

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public GameObject questionPanel;
    public int NbQuestion;
    private Parties questionData;
    public int currentQuestionIndex;
    public int currentPartieIndex;
    public bool Repondu = false;
    public GameManager _GM;
    void Start()
    {
        LoadQuestionsFromJSON();
        currentQuestionIndex = 0;
        currentPartieIndex = _GM.partie;
        NbQuestion = questionData.parties[currentPartieIndex].questions.Length;
        Debug.Log(NbQuestion);

        DisplayQuestion(currentQuestionIndex);
    }

    void LoadQuestionsFromJSON()
    {
        string jsonPath = "Assets/StreamingAssets/questionnaire.json"; // Chemin vers le fichier JSON
        string json = File.ReadAllText(jsonPath);
        questionData = JsonUtility.FromJson<Parties>(json);
    }

    public void DisplayQuestion(int index)
    {
        // Afficher la question et ses réponses en fonction de l'index actuel et de la partie actuelle
        questionText.text = questionData.parties[currentPartieIndex].questions[index].questionText;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            // Vérifier si la réponse existe pour l'index donné dans le tableau de réponses
            if (i < questionData.parties[currentPartieIndex].questions[index].choices.Length)
            {
                answerTexts[i].text = questionData.parties[currentPartieIndex].questions[index].choices[i];
            }
            else
            {
                // Si on a dépassé le nombre de réponses disponibles, cacher le texte de réponse
                answerTexts[i].gameObject.SetActive(false);
            }
        }
    }


    public void NextQuestion()
    {
        currentQuestionIndex++;
        Repondu = true;
        Debug.Log("Je suis a la question : " + currentQuestionIndex);

        if (currentQuestionIndex >= NbQuestion)
        {
            // Si on a répondu à toutes les questions de la partie actuelle, passer à la partie suivante
            currentPartieIndex++;
            _GM.SavePartie(currentPartieIndex);
            if (currentPartieIndex < questionData.parties.Length)
            {
                currentQuestionIndex = 0;
                NbQuestion = questionData.parties[currentPartieIndex].questions.Length;
            }
            else
            {
                // Si on a fini toutes les parties, peut-être afficher un message ou faire quelque chose d'autre
                Debug.Log("Fin du questionnaire !");
                return;
            }
        }

        DisplayQuestion(currentQuestionIndex);
    }
}