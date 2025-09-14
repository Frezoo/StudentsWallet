using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGameSetup : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Toggle motorcycleGoalToggle;
    public Toggle laptopGoalToggle;

    

    public void OnMotorcycleGoalSelected()
    {
        // ����� �������� ���� "��������", ��������� ���� "�������"
        laptopGoalToggle.isOn = false;
    }

    public void OnLaptopGoalSelected()
    {
        // ����� �������� ���� "�������", ��������� ���� "��������"
        motorcycleGoalToggle.isOn = false;
    }

    public void StartGame()
    {
        string playerName = nameInput.text;

        // �������� �� ������ ���
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("��� �� ����� ���� ������!");
            return; // ��������� ���������� ������
        }

        string selectedGoal = "";

        if (motorcycleGoalToggle.isOn)
        {
            selectedGoal = "���������� �� ��������";
        }
        else if (laptopGoalToggle.isOn)
        {
            selectedGoal = "������ �������";
        }
        else
        {
            Debug.LogError("�� ������� ����!");
            return; // ��������� ���������� ������
        }

        // �������� ������ � GameManager
        GameManager.Instance.PlayerName = playerName;
        GameManager.Instance.SelectedGoal = selectedGoal;

        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetString("Goal", selectedGoal);


        // ������������� ����, ��� ���� ���� ����������������
        GameManager.Instance.SetupInitialGameState();
        PlayerPrefs.SetInt("GameInitialized", 1);
        PlayerPrefs.Save();
        // ��������� �������� �����
        SceneManager.LoadScene("GameScene");
    }
}