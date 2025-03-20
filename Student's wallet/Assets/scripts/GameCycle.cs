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
    public float Psycho;
    public float Grade;
    public float Energy;
    [Header("Время")]
    public float Hourse;
    public float Minuts;
    [Header("Интерфейс")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TimeText;

    public TextMeshProUGUI EnergyCountText;
    public TextMeshProUGUI GradeCountText;
    public TextMeshProUGUI PsychoCountText;
    public TextMeshProUGUI HungryCountText;

    public Image HungerFront;
    public Image PsychoFront;
    public Image EnergyFront;
    public Image GradeFront;

    public GameObject ActionObj;

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
        //Money = PlayerPrefs.GetFloat("");
        Hungry = PlayerPrefs.GetFloat("");
        Psycho = PlayerPrefs.GetFloat("");
        Name = PlayerPrefs.GetString("");
    }
    
    void UpdateUI()
    {
        //MoneyText.text = $"{Money}$";
        TimeText.text = $"{Hourse}:{Minuts}";

        EnergyCountText.text = Energy.ToString();
        HungryCountText.text = Hungry.ToString();
        GradeCountText.text = Grade.ToString();
        PsychoCountText.text = Psycho.ToString();

        HungerFront.fillAmount = Hungry/100;
        PsychoFront.fillAmount = Psycho/100;
        EnergyFront.fillAmount = Energy / 100;
        GradeFront.fillAmount = Grade / 100;
    }
    
    void StatisticClamp()
    {
        Hungry = Mathf.Clamp(Hungry,0, 100);
        Psycho = Mathf.Clamp(Psycho, 0, 100);
        Energy = Mathf.Clamp(Energy, 0, 100);
        Grade = Mathf.Clamp(Grade, 0, 100);

        Hourse = Mathf.Clamp(Hourse, 0, 24);
        Minuts = Mathf.Clamp(Minuts, 0, 60);
    }

    public void ToggelActionPanel()
    {
        ActionObj.SetActive(!ActionObj.activeSelf);
    }
}
