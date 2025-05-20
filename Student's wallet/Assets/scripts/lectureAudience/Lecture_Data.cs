using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lecture1", menuName = "Scriptable Objects/Lecture1")]
public class Lecture1 : ScriptableObject
{
    public string lectureTitle;

    // Предисловие
    public string preface;

    // Список текстов для каждого слайда
    public List<string> slides = new List<string>();

    // Вопросы с вариантами ответов
    public List<Question> questions = new List<Question>();
}

[System.Serializable]
public class Question
{
    public string questionText; // Текст вопроса
    public List<string> answerOptions = new List<string>(); // Варианты ответов
    public int correctAnswerIndex; // Индекс правильного ответа (начиная с 0)
}
