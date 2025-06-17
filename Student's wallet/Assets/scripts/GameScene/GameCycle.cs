using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCycle : MonoBehaviour
{

    [Header("Интерфейс")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI DayText;

    public TextMeshProUGUI EnergyCountText;
    public TextMeshProUGUI GradeCountText;
    public TextMeshProUGUI PsychoCountText;

    public Image PsychoFront;
    public Image EnergyFront;
    public Image GradeFront;

    public GameObject ActionObj;

    [Header("Fade")]
    public Image fadeImage; // Image for the fade effect
    public float fadeAlpha = 1f;  // Maximum alpha value during fade

    private Coroutine fadeCoroutine;

    void Start()
    {
        NameText.text = GameManager.Instance.PlayerName;
        Time.timeScale = 1f;
        if (fadeImage != null)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = 0;
            fadeImage.color = imageColor;
        }
    }

    void Update()
    {
        UpdateUI();
        StatisticClamp();
    }

    void UpdateUI()
    {

        TimeText.text = $"{GameManager.Instance.Hourse.ToString().PadLeft(2, '0')}:{GameManager.Instance.Minuts.ToString().PadLeft(2, '0')}";
        MoneyText.text = $"{GameManager.Instance.Money}р";
        DayText.text = GameManager.Instance.Day.ToString();

        EnergyCountText.text = GameManager.Instance.Energy.ToString();

        GradeCountText.text = GameManager.Instance.Grade.ToString();
        PsychoCountText.text = GameManager.Instance.Psycho.ToString();


        PsychoFront.fillAmount = GameManager.Instance.Psycho / 100f; 
        EnergyFront.fillAmount = GameManager.Instance.Energy / 100f;  
        GradeFront.fillAmount = GameManager.Instance.Grade / 100f;  
    }

    void StatisticClamp()
    {
        GameManager.Instance.Psycho = Mathf.Clamp(GameManager.Instance.Psycho, 0, 100);
        GameManager.Instance.Energy = Mathf.Clamp(GameManager.Instance.Energy, 0, 100);
        GameManager.Instance.Grade = Mathf.Clamp(GameManager.Instance.Grade, 0, 100);
        GameManager.Instance.Hourse = Mathf.Clamp(GameManager.Instance.Hourse, 0, 24);
        GameManager.Instance.Minuts = Mathf.Clamp(GameManager.Instance.Minuts, 0, 60);
    }

    public void ToggelActionPanel()
    {
        ActionObj.SetActive(!ActionObj.activeSelf);
    }

    public void FadeScreen(float displayDuration, float fadeDuration)
    {
        fadeImage.gameObject.SetActive(true);
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeRoutine(displayDuration, fadeDuration));
        
    }

    private IEnumerator FadeRoutine(float displayDuration, float fadeDuration)
    {

        float time = 0;
        Color imageColor = fadeImage.color;
        float startAlpha = 0f;
        float endAlpha = fadeAlpha;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            imageColor.a = alpha;
            fadeImage.color = imageColor;
            yield return null;
        }

        imageColor.a = endAlpha;
        fadeImage.color = imageColor;


        yield return new WaitForSeconds(displayDuration);

        time = 0;
        startAlpha = endAlpha;
        endAlpha = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            imageColor.a = alpha;
            fadeImage.color = imageColor;
            yield return null;
        }

        imageColor.a = endAlpha;  
        fadeImage.color = imageColor;

        fadeCoroutine = null;
        fadeImage.gameObject.SetActive(false);
    }
}