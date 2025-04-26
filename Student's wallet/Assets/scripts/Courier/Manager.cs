using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    [Header("Дороги и точки")]
    public List<GameObject> Roads;
    public List<GameObject> Dots;

    [Header("Конфигурация")]
    public int TimeBetweenChangeTrafic;
    private bool isOrderAccepted;
    [Range(0, 100)]
    public int probability1 = 60;
    [Range(0, 100)]
    public int probability2 = 30;
    [Range(0, 100)]
    public int probability3 = 10;

    [Header("Statistics")]
    private float timeFromStart;
    private int potentialAward;
    private int award;

    [Header("UI")]
    public TextMeshProUGUI TimeFromStart;
    public TextMeshProUGUI PotentialAward;
    public TextMeshProUGUI AwardText;
    public TextMeshProUGUI Hint;

    [Header("Images")]
    public Sprite DotSprite;
    public Sprite RestourantSprite;
    public Sprite ClientHouseSprite;

    public GameObject AcceptOrderButton;


    private GameObject restourantDot;
    private GameObject clientDot;

    private Color color1 = Color.green;
    private Color color2 = Color.yellow;
    private Color color3 = Color.red;

    private void Start()
    {
        StartCoroutine(ChangeRoadColorsCoroutine());
    }
    private void Update()
    {
        UpdateUI();
        if (isOrderAccepted)
        {
            timeFromStart += Time.deltaTime;
            potentialAward = GetPotentialAward();
        }

        

    }

    private int GetPotentialAward()
    {
        if (timeFromStart >= 0f && timeFromStart <= 10f)
        {
            return  550;
        }
        else if (timeFromStart >= 10.01f && timeFromStart <= 20f)
        {
            return 500;
        }
        else if (timeFromStart >= 20.01f && timeFromStart <= 30f)
        {
            return 400;
        }
        else if (timeFromStart >= 30.01f && timeFromStart <= 40f)
        {
            return 300;
        }
        else if (timeFromStart >= 40.01f && timeFromStart <= 50f)
        {
            return 200;
        }
        else if (timeFromStart >= 50.01f)
        {
            return 100;
        }
        return 0;
    }

    public void StartOrder()
    {
        if (!isOrderAccepted)
        {
            isOrderAccepted = true;
            timeFromStart = 0;
            AcceptOrderButton.SetActive(false);
            SetDestonation();
            Hint.text = "Заберите заказ";
        }
    }

    public void SetDestonation()
    {
        (restourantDot, clientDot) = GetTwoDifferentRandomPoints();
        restourantDot.GetComponent<SpriteRenderer>().sprite = RestourantSprite;
        clientDot.GetComponent<SpriteRenderer>().sprite = ClientHouseSprite;

        restourantDot.tag = "Restourant";
        clientDot.tag = "Client";
    }

    void UpdateUI()
    {
        TimeFromStart.text = $"Время с начала заказа:{Mathf.Round(timeFromStart)}";
        PotentialAward.text = $"Потенциалньая прибыль:{potentialAward}$";
        AwardText.text = $"Заработано за смену:{award}$";
    }

    public void LeaveJob()
    {
        GameManager.Instance.Money += award;
        SceneManager.LoadSceneAsync(1);
    }

    public void SumbitOrder()
    {
        award += potentialAward;

        restourantDot.GetComponent<SpriteRenderer>().sprite = DotSprite;
        clientDot.GetComponent<SpriteRenderer>().sprite = DotSprite;

        restourantDot.tag = "Untagged";
        clientDot.tag = "Untagged";

        timeFromStart = 0;

        AcceptOrderButton.SetActive(true);
        isOrderAccepted = false;

        Hint.text = "";
    }

    public (GameObject, GameObject) GetTwoDifferentRandomPoints()
    {
        List<GameObject> availablePoints = new List<GameObject>(Dots);
        int firstIndex = Random.Range(0, availablePoints.Count);
        GameObject RestourantDot = availablePoints[firstIndex];
        availablePoints.RemoveAt(firstIndex);
        int secondIndex = Random.Range(0, availablePoints.Count);
        GameObject ClientDot = availablePoints[secondIndex];
        return (RestourantDot, ClientDot);
    }


    IEnumerator ChangeRoadColorsCoroutine()
    {
        while (true)
        {
            ChangeRoadColors();
            yield return new WaitForSeconds(TimeBetweenChangeTrafic);
        }
    }

    void ChangeRoadColors()
    {
        // Используем LINQ для перебора объектов и изменения цвета
        Roads.Where(roadSegment => roadSegment != null) // Фильтруем null объекты
            .Select(roadSegment => roadSegment.GetComponent<SpriteRenderer>()) // Получаем SpriteRenderer
            .Where(spriteRenderer => spriteRenderer != null) // Фильтруем объекты без SpriteRenderer
            .ToList() // Преобразуем в список (для итерации)
            .ForEach(spriteRenderer =>
            {
                int randomNumber = Random.Range(1, 101);
                Color newColor = color1;

                if (randomNumber > probability1)
                {
                    newColor = color2;
                }
                if (randomNumber > (probability1 + probability2))
                {
                    newColor = color3;
                }

                spriteRenderer.color = newColor;
            });
    }
}
