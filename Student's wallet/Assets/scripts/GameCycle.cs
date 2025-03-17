using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCycle : MonoBehaviour
{
    [Header("Статистика")]
    public string Name;
    public float Money;
    public float Hungry;
    public float Mind;
    [Header("Интерфейс")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI NameText;
    public Image HungerScale;
    public Image MindScale;

    private void Awake()
    {
        Initizializate();
    }

    void Start()
    {
        NameText.text = Name;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        StatisticClamp();
    }

    void Initizializate()
    {
        Money = PlayerPrefs.GetFloat("");
        Hungry = PlayerPrefs.GetFloat("");
        Mind = PlayerPrefs.GetFloat("");
        Name = PlayerPrefs.GetString("");
    }
    
    void UpdateUI()
    {
        MoneyText.text = $"{Money}$";
        HungerScale.fillAmount = Hungry;
        MindScale.fillAmount = Mind;
    }
    
    void StatisticClamp()
    {
        Hungry = Mathf.Clamp(Hungry,0, 1);
        Mind = Mathf.Clamp(Mind, 0, 1);
    }
}
