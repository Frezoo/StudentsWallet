using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BedLogic : MonoBehaviour
{
    [Header("UI")]
    public GameObject BedPanel;
    public Image SleepImage;
    public Slider sleepTimeSlider; // Ссылка на слайдер
    public TextMeshProUGUI sleepTimeText; // Время сна
    public TextMeshProUGUI energyChangeText; // Изменение энергии
    public TextMeshProUGUI psychoChangeText; // Изменение психики

    [Header("Configurations")]
    public List<int> SleepAcceptableTime;
    public GameCycle GC;

    public bool Sleep;

    private int selectedSleepTime = 60; // По умолчанию — 60 минут

    public void Start()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();

        // Настройка слайдера
        if (sleepTimeSlider != null)
        {
            sleepTimeSlider.minValue = 15;
            sleepTimeSlider.maxValue = 120;
            sleepTimeSlider.wholeNumbers = true;
            sleepTimeSlider.value = selectedSleepTime;

            sleepTimeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        UpdateSleepTimeText();
        UpdateStatsPreview();
    }

    private void OnMouseDown()
    {
        BedPanel.SetActive(true);
        UpdateStatsPreview(); // Обновляем значения при открытии панели
    }

    void MakeSleep()
    {
        if (SleepAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            Debug.Log($"Вы поспали {selectedSleepTime} минут");

            Sleep = true;

            // Меняем игровое время
            GameManager.Instance.ChangeTime(selectedSleepTime / 60, selectedSleepTime % 60);

            // Энергия: +10 за 60 минут → +0.1667 в минуту
            GameManager.Instance.Energy += Mathf.RoundToInt(0.1667f * selectedSleepTime);

            // Психика: +2 за 60 минут → +0.0333 в минуту
            GameManager.Instance.Psycho += Mathf.RoundToInt(0.0333f * selectedSleepTime);

            // Рассчитываем длительность FadeScreen
            float fadeDuration = 0.0333f * selectedSleepTime;
            GC.FadeScreen(fadeDuration, 1); // Вызываем анимацию затухания

            Sleep = false;
        }
    }

    public void GoSleep()
    {
        if (!Sleep)
        {
            MakeSleep();
        }
    }

    public void ClosePanel()
    {
        BedPanel.SetActive(false);
    }

    // Обработчик изменения слайдера
    void OnSliderValueChanged(float value)
    {
        selectedSleepTime = Mathf.RoundToInt(value);
        UpdateSleepTimeText();
        UpdateStatsPreview();
    }

    // Обновляем текст рядом со слайдером
    void UpdateSleepTimeText()
    {
        if (sleepTimeText != null)
        {
            sleepTimeText.text = $"{selectedSleepTime} мин.";
        }
    }

    // Обновляем предпросмотр характеристик
    void UpdateStatsPreview()
    {
        int energyChange = Mathf.RoundToInt(0.1667f * selectedSleepTime);
        int psychoChange = Mathf.RoundToInt(0.0333f * selectedSleepTime);

        if (energyChangeText != null)
        {
            energyChangeText.text = $"+{energyChange}en.";
        }

        if (psychoChangeText != null)
        {
            psychoChangeText.text = $"+{psychoChange}psy.";
        }
    }
}