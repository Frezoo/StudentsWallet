using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MarketLogic : MonoBehaviour
{
    [Header("Fruit Counters")]
    public int GoodFruitCount;
    public int BadFruitCount;
    public int MaxFruitCount;
    public int MaxBadFruitCount;

    [Header("Collected Fruit Counters")]
    public int FruitOutScreen;
    public int CollectedBadFruitByPlayer => PlayerScript.BadEatCount;
    public int CollectedGoodFruitByPlayer => PlayerScript.GoodEatCount;

    [Header("UI")]
    public TextMeshProUGUI EatCountText;
    public Image HealthPanel;
    public GameObject FinalPanel;

    public TextMeshProUGUI FinalPanelGoodEatText;
    public TextMeshProUGUI FinalPanelBadEatText;
    public TextMeshProUGUI FinalPanelMissText;
    public TextMeshProUGUI FinalPanelAwardText;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI PlayerMoneyText;
    public GameObject InitPanel;

    [Header("Timers")]
    private float timeSinceLastSpawn;
    private float timeBetweenSpawns;

    [Header("Prefabs")]
    public GameObject GoodFruit;
    public GameObject BadFruit;

    [Header("Configuration")]
    public float FruitSpeed;
    public float FruitSpeedMultiplier;
    public float AwardCount;

    public bool GameOver;

    public int Money;



    public Slider SldierPrice;

    [Header("Other")]
    public List<Transform> FruitSpawnPoints;
    public MarketPlayerController PlayerScript;



    public int fruitsPerSpawn = 2;

    private void Start()
    {
        InitializeLevel(Money);
    }

    void Update()
    {
        UpdateStatistic();

        if ((PlayerScript.Health > 0 && (CollectedBadFruitByPlayer + CollectedGoodFruitByPlayer + FruitOutScreen) < MaxFruitCount))
        {
            EatSpanwer();
        }
        else
        {
            EndGame();
        }
    }

    private void EatSpanwer()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            for (int i = 0; i < fruitsPerSpawn; i++)
            {
                if (GoodFruitCount + BadFruitCount >= MaxFruitCount) break;

                var eatType = Random.Range(0, 2);

                if (eatType == 1 && BadFruitCount < MaxBadFruitCount)
                {
                    SpawnFruit(BadFruit);
                    BadFruitCount++;
                }
                else
                {
                    SpawnFruit(GoodFruit);
                    GoodFruitCount++;
                }
            }
            timeSinceLastSpawn = 0f;
            timeBetweenSpawns = Random.Range(0.5f, 1f);
        }
    }

    private void EndGame()
    {
        if (!GameOver)
        {
            GameOver = true;
            Time.timeScale = 0.01f;
            if (PlayerScript.Health != PlayerScript.MaxHealth)
            {
                AwardCount = Mathf.FloorToInt(AwardCount * (PlayerScript.Health / 3f));
            }

            GameOver = true;
            FinalPanel.SetActive(true);
            FinalPanelGoodEatText.text = $"Подобрано свежей еды:{PlayerScript.GoodEatCount}";
            FinalPanelBadEatText.text = $"Подобрано испорченной еды:{PlayerScript.BadEatCount}";
            FinalPanelMissText.text = $"Пропущено еды:{BadFruitCount + GoodFruitCount - PlayerScript.BadEatCount - PlayerScript.GoodEatCount}";
            FinalPanelAwardText.text = $"Итоговая награда:{AwardCount} ед. Еды";

            PlayerPrefs.SetInt("FrideEat",PlayerPrefs.GetInt("FrideEat") + (int)AwardCount);
        }
    }

    void SpawnFruit(GameObject Eat)
    {
        timeBetweenSpawns = Random.Range(0.5f, 1.5f);
        var Fruit = (GameObject)Instantiate(Eat, FruitSpawnPoints[Random.Range(0, FruitSpawnPoints.Count)]);
        Fruit.GetComponent<EatLogic>().Speed = FruitSpeed + Random.Range(-0.6f, 0.6f);
        timeSinceLastSpawn = 0;
    }

    void UpdateStatistic()
    {
        HealthPanel.fillAmount = (float)PlayerScript.Health / PlayerScript.MaxHealth;
        EatCountText.text = $"{GoodFruitCount + BadFruitCount}/{MaxFruitCount}";
    }

    public void GoBack()
    {
        SceneManager.LoadScene(1);
    }

    public void InitializeLevel(int spendingAmount)
    {
        var _spendingAmount = spendingAmount; // Сохраняем потраченную сумму
        FruitSpeedMultiplier = 1f; // Устанавливаем множитель скорости по умолчанию

        if (spendingAmount >= 100 && spendingAmount <= 350)
        {
            // Легкий уровень
            MaxFruitCount = 10; // Устанавливаем максимальное количество фруктов
            MaxBadFruitCount = 3; // Устанавливаем максимальное количество плохих фруктов
            AwardCount = 4; // Устанавливаем базовую награду
        }
        else if (spendingAmount >= 351 && spendingAmount <= 750)
        {
            // Средний уровень
            FruitSpeedMultiplier = 1.5f; // Увеличиваем множитель скорости
            MaxFruitCount = 20; // Увеличиваем количество фруктов
            MaxBadFruitCount = 8; // Увеличиваем количество плохих фруктов
            AwardCount = 8; // Увеличиваем базовую награду
        }
        else if (spendingAmount >= 751 && spendingAmount <= 1000)
        {
            // Сложный уровень
            FruitSpeedMultiplier = 2f; // Увеличиваем множитель скорости
            MaxFruitCount = 45; // Увеличиваем количество фруктов
            MaxBadFruitCount = 15; // Увеличиваем количество плохих фруктов
            AwardCount = 15; // Увеличиваем базовую награду
        }


        FruitSpeed *= FruitSpeedMultiplier; // Применяем множитель к базовой скорости
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FruitOutScreen++;
        Destroy(collision);
    }

    public void StartGame()
    {

    }

}
