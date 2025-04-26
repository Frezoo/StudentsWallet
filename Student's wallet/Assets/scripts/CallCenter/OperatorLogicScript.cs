using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OperatorLogicScript : MonoBehaviour
{
    [Header("�����")]
    public TextMeshProUGUI Question;
    public TextMeshProUGUI Answer1;
    public TextMeshProUGUI Answer2;
    public TextMeshProUGUI Answer3;

    [Header("������")]
    public List<GameObject> Buttons;

    [Header("�����������")]
    public Image TimerIMG;

    [Header("������� � ������")]

    public string[] questions = {
 "� ����� ����� ������� 5000 ���., �� � ������ �� �������!",
 "������ � ���� ��������������� ����� ��� ��������������?",
 "��� ������ ���� �� ������, ������� � �� �����������!",
 "������ ����� �������� ������� ��������� ��������?",
 "���� �������� �� �� ���� ��������� ��� ������ ���!",
 "��� ��� ���������� ������� �� ������� �� �������?",
 "���� ������� �����, �� �� ���� ����� ���������",
 "������ � ���� ���������� �������� ������ �����?",
 "��� ��������� ������������� ��������?",
 "����� �� ��������� �������� ��������� ��� ����� ������?",
 "� ���� �� �������� ��������� ��������",
 "������ � �� ���� ����� � ������ �������?",
 "������ ����������� ����� 10 ������",
 "SMS �� ������������ ��� ���",
 "���������� ��������� ��������",
 "� ��� ������ ��� 30 �����! ��� ����������!",
 "���� ��� ����� �������� �����������!",
 "�� ��� �������� � ������� ���!",
 "� ������ ����������� �� ���� ������!",
 "� ����� � ��� �� ��� �����!",
 "� ������� �������, ��� ������������ SIM-�����?",
 "��� ������ ��������� �� ������ �����!",
 "��� ������ ����� �������� �� 10 000 ���.!",
 "� �������� � ������ �����, ��� ������ � �������?",
 "�� ��� ������� ������, �� � ���!"
};

    public string[][] answers = {
 new string[] { "������ ������� ���������� � ������ ��� �����������", "��� ���� ��������", "��������, �� ���� ���-�� ������" },
 new string[] { "������� ������� ���������� � ������ ������������ ������", "����� �� ����, ������� � ����", "��� ����������� ����, ��������� ������" },
 new string[] { "�������� �� �������������, ������ ��������� � ������ ����������", "��������, ����� ���������", "��� �������������� ��������, ������ �� ���������" },
 new string[] { "������� ��������� ���� � �������� �����������", "��� �� ���� ��������", "��������, � ��� ������ ������" },
 new string[] { "������ ���� ��� ������ �����, ����� �� �����������", "������� ��������������, ������ �� ���������", "����������� �����" },
 new string[] { "������ ��������, ��� ����� ������� ����� ���������� ��� SMS-�������", "��� ������, ����� �� �����������", "������� � ��������� �� ��������" },
 new string[] { "������ ��������� ���������� ����� � ��������, ��� ��� ������������", "����� ������� ���, ��������� �� ������", "��� ����� ������� ������ � �����" },
 new string[] { "������� ���������� � �������� �������������� ������", "�� ������� ����� �������", "��� ���� ����" },
 new string[] { "������ �������, ��� ��� ������� � ������ ��������", "��� ����������, ���� �� ���������� ����", "������ �� ����� ���������" },
 new string[] { "��, ���� ������� �����, �������� ���������", "���, ������ ���� �������� �� ������� �����", "�������� �������������, ������ �� ��������" },
 new string[] { "������� ��������� � �������� ��� ���������� ��� ��������������", "������������� ������� � �� ���������� ���", "��� �������� ������ ��������" },
 new string[] { "������ ������������ ������: ��������� ����� ��� ����", "���� �������� �� ��������", "��� �������, ��������, �����" },
 new string[] { "������� �������� ����� � ����� ������", "������ ����� �������", "��� �� ���� ���� ��������" },
 new string[] { "������� ��������� ���� � �������", "���������, �����, ���� ����������", "��� ������� �� ������������ SMS" },
 new string[] { "��������� �������� ��� ��� ��������������", "������ ����� �������", "��� �� ���� ��������" },
 new string[] { "����� ��������� �� ��������, ��� � ���� ��� ������?", "������� �������, ��� �� ������?", "���� �������, � �� ���� ��������" },
 new string[] { "������ ���� ��� ������ �����, ����� �� ����������", "������� ��������������, �������", "����������� � ������ �����" },
 new string[] { "������� ���������, ������� ����� ��� ������ ������", "��� ��� �� �", "���� ��� �� �������� � �������" },
 new string[] { "���������� ���� ��������� � ��������� �������", "����������� �� �������������", "�� ���� ��������" },
 new string[] { "������� ������� ��������, ����� �������� �������������", "������� ��� ������", "��� ����� ��� �������" },
 new string[] { "������ �������� ��������, ����� �������", "��� �������� ������", "��������� � ���� (���� ���� ������ �� ��������)" },
 new string[] { "����������� � ����� ������ ������", "�� ��� �� ��� ���", "������ �� ���������" },
 new string[] { "������ �������� �������, ����� ������ �����", "�������� ������ ������� �� ������", "������ �� �����" },
 new string[] { "������� ����������� ������� ��� ������ �������", "������� ��� ������", "������� �����" },
 new string[] { "������� ������ ��������� � �������� ��������", "��� ������ �� ��������", "������ �����������" }
};

    public int[] correctAnswers = {
 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

    [Header("������������")]

    public int CurrentQuestionIndex = 0;

    public int MoneyAward = 0;

    public int CountCorrectAnswerInTime;
    public int CountCorrectAnswerOutTime;
    public int CountUncorrectAnswer;

    public float TimeToAnswer = 10f;
    public float InitialTimeToAnswer = 10f;

    public Color EndColor;
    public Color StartColor;

    [Header("������")]
    public EndGamePanel EGP;


    private void Start()
    {
        EGP = GetComponent<EndGamePanel>();
        LoadQuestion(0);
    }

    private void Update()
    {
        TimeToAnswer -= Time.deltaTime;
        UpdateTimerImage();
    }

    void UpdateTimerImage()
    {
        float fillAmount = TimeToAnswer / InitialTimeToAnswer;
        TimerIMG.fillAmount = fillAmount;
        TimerIMG.color = Color.Lerp(EndColor, StartColor, fillAmount);
    }

    void LoadQuestion(int questionIndex)
    {
        if (questionIndex >= questions.Length)
        {
            EGP.GetConfigValues(CountCorrectAnswerInTime, CountCorrectAnswerOutTime, CountUncorrectAnswer, MoneyAward);
            EGP.TurnEndPanel();
            return;
        }
        ShuffleButtonPositions();
        Question.text = questions[questionIndex];
        Answer1.text = answers[questionIndex][0];
        Answer2.text = answers[questionIndex][1];
        Answer3.text = answers[questionIndex][2];
    }

    public void AnswerButtonClicked(int answerIndex)
    {
        if (answerIndex == correctAnswers[CurrentQuestionIndex])
        {
            if (TimeToAnswer >= 0)
            {
                Debug.Log("���������� �����!");
                MoneyAward += 400;
                CountCorrectAnswerInTime++;
            }
            else
            {
                Debug.Log("���������� �����,�� ����� �� ���������!");
                MoneyAward += 200;
                CountCorrectAnswerOutTime++;
            }
            
            
        }
        else
        {
            Debug.Log("������������ �����!");
            CountUncorrectAnswer++;
        }

        CurrentQuestionIndex++;
        TimeToAnswer = InitialTimeToAnswer;
        LoadQuestion(CurrentQuestionIndex);
    }

    public void ShuffleButtonPositions()
    {

        List<Vector3> positions = Buttons.Select(b => b.transform.position).ToList();

        positions = positions.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].transform.position = positions[i];
        }
    }

}
