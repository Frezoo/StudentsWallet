using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string playerName;
    public string selectedGoal;

    private void Awake()
    {
        // ���� ��� ���� GameManager, ���������� ���� ������
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // ��� ������ GameManager, ������ ��� Instance
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ��� �������� ����� �������
        }
    }
}