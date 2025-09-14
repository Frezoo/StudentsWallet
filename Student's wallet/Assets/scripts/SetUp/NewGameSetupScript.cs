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
        // Когда выбираем цель "Мотоцикл", отключаем цель "Ноутбук"
        laptopGoalToggle.isOn = false;
    }

    public void OnLaptopGoalSelected()
    {
        // Когда выбираем цель "Ноутбук", отключаем цель "Мотоцикл"
        motorcycleGoalToggle.isOn = false;
    }

    public void StartGame()
    {
        string playerName = nameInput.text;

        // Проверка на пустое имя
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Имя не может быть пустым!");
            return; // Прерываем выполнение метода
        }

        string selectedGoal = "";

        if (motorcycleGoalToggle.isOn)
        {
            selectedGoal = "Заработать на мотоцикл";
        }
        else if (laptopGoalToggle.isOn)
        {
            selectedGoal = "Купить ноутбук";
        }
        else
        {
            Debug.LogError("Не выбрана цель!");
            return; // Прерываем выполнение метода
        }

        // Передаем данные в GameManager
        GameManager.Instance.PlayerName = playerName;
        GameManager.Instance.SelectedGoal = selectedGoal;

        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetString("Goal", selectedGoal);


        // Устанавливаем флаг, что игра была инициализирована
        GameManager.Instance.SetupInitialGameState();
        PlayerPrefs.SetInt("GameInitialized", 1);
        PlayerPrefs.Save();
        // Загружаем основную сцену
        SceneManager.LoadScene("GameScene");
    }
}