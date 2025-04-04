using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketLogic : MonoBehaviour
{
    [Header("Fruit Counters")]
    public int GoodFruitCount;
    public int BadFruitCount;
    public int MaxFruitCount;
    public int MaxBadFruitCount;

    [Header("UI")]
    public TextMeshProUGUI EatCountText;
    public Image HealthPanel;

    [Header("Timers")]
    private float timeSinceLastSpawn;
    private float timeBetweenSpawns;

    [Header("Prefabs")]
    public GameObject GoodFruit;
    public GameObject BadFruit;

    [Header("Configuration")]
    public float FruitSpeed;
    public float FruitSpeedMultiplier;

    [Header("Other")]
    public List<Transform> FruitSpawnPoints;
    public MarketPlayerController PlayerScript;

    void Start()
    {
        FruitSpeed = FruitSpeed * FruitSpeedMultiplier;
    }


    void Update()
    {
        if (PlayerScript.Health > 0 && GoodFruitCount + BadFruitCount < MaxFruitCount)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= timeBetweenSpawns)
            {
                var eatType = Random.Range(0, 2);

                if (eatType == 1 && BadFruitCount < MaxBadFruitCount)
                {
                    SpawnFruit(BadFruit);
                    BadFruitCount++;
                    timeSinceLastSpawn = 0f;
                    timeBetweenSpawns = Random.Range(0.5f, 1.5f);
                }
                else
                {
                    SpawnFruit(GoodFruit);
                    GoodFruitCount++;
                    timeSinceLastSpawn = 0f;
                    timeBetweenSpawns = Random.Range(0.5f, 1.5f);
                }



            }
        }
    }

    void SpawnFruit(GameObject Eat)
    {
        timeBetweenSpawns = Random.Range(0.5f, 1.5f);
        var Fruit = (GameObject)Instantiate(Eat, FruitSpawnPoints[Random.Range(0, FruitSpawnPoints.Count)]);
        Fruit.GetComponent<EatLogic>().Speed = FruitSpeed + Random.Range(-0.4f, 0.4f);
        timeSinceLastSpawn = 0;
    }

    void UpdateStatistic()
    {

    }
}
