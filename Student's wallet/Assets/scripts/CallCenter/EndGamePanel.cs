using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    public GameObject EndPanel;
    [Header("UI")]
    public TextMeshProUGUI CorrectAnswerInTime;
    public TextMeshProUGUI CorrectAnswerOutTime;
    public TextMeshProUGUI UncorrectTime;
    public TextMeshProUGUI AwardText;

    [Header("Config")]
    public int CountOfCorrectAnswerInTime;
    public int CountOfCorrectAnswerOutTime;
    public int CountOfUncorrectAnswer;
    public int Award;

    public void TurnEndPanel()
    {
        CorrectAnswerInTime.text = $"���������� ������� �������:{CountOfCorrectAnswerInTime}";
        CorrectAnswerOutTime.text = $"���������� ������� �� �������::{CountOfCorrectAnswerOutTime}";
        UncorrectTime.text = $"������������ �������:{CountOfUncorrectAnswer}";
        AwardText.text = $"����������:{Award}$";
        EndPanel.SetActive(true);
    }

    public void EndGame()
    {
        GameManager.Instance.Money += Award;
        SceneManager.LoadSceneAsync(1);
    }

    public void GetConfigValues(int countOfCorrectAnswerInTime,int countOfCorrectAnswerOutTime,int countOfUncorrectAnswer,int award)
    {
        CountOfCorrectAnswerInTime = countOfCorrectAnswerInTime;
        CountOfCorrectAnswerOutTime = countOfCorrectAnswerOutTime;
        CountOfUncorrectAnswer = countOfUncorrectAnswer;
        Award = award;
    }

}
