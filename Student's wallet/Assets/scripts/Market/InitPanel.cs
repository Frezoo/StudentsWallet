using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitPanel : MonoBehaviour
{
    [Header("UI")]
    public Slider priceSlider; // —сылка на слайдер
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI WarningText;

    [Header("Configuration")]
    public float priceMultiplier = 1000f; // ћножитель дл€ цены
    float tempMoney;
    int price;

    [Header("Other")]
    public MarketLogic marketLogic; // —сылка на MarketLogic
    public GameObject initPanel;




    private void Update()
    {

        ClampSliderValue();
        price = Mathf.RoundToInt(priceSlider.value * priceMultiplier);
        tempMoney = GameManager.Instance.Money - price;

        PriceText.text = price.ToString() + "$";
        MoneyText.text = tempMoney.ToString() + "$";

    }

    public void StartGame()
    {
        if (tempMoney <= 0)
        {
            WarningText.text = "Not enough money";
        }
        else
        {
            GameObject.Find("MarketManager").GetComponent<MarketLogic>().Money = price;
            gameObject.SetActive(false);
            GameObject.Find("MarketManager").GetComponent<MarketLogic>().enabled = true;
            GameManager.Instance.Money = tempMoney;
        }
    }
    public void GoBack()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ClampSliderValue()
    {
        priceSlider.value = Mathf.Clamp(priceSlider.value, 0.1f, 1f);
    }

}