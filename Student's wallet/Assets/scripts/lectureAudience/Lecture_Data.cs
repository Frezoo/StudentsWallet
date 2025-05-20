using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lecture1", menuName = "Scriptable Objects/Lecture1")]
public class Lecture1 : ScriptableObject
{
    public string lectureTitle;

    // �����������
    public string preface;

    // ������ ������� ��� ������� ������
    public List<string> slides = new List<string>();

    // ������� � ���������� �������
    public List<Question> questions = new List<Question>();
}

[System.Serializable]
public class Question
{
    public string questionText; // ����� �������
    public List<string> answerOptions = new List<string>(); // �������� �������
    public int correctAnswerIndex; // ������ ����������� ������ (������� � 0)
}
