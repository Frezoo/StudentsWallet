using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string playerName;
    public string selectedGoal;

    private void Awake()
    {
        // Если уже есть GameManager, уничтожаем этот объект
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Это первый GameManager, делаем его Instance
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при переходе между сценами
        }
    }
}