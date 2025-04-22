using UnityEngine;
using UnityEngine.UI;

public class FridgeLogic : MonoBehaviour
{

    [Header("Fridge settings")]
    public int MaxEat;
    public int CurrentEatCount;

    public int EatToEnergyScale;


    [Header("Fridge UI")]
    public Image EatCountFrontImage;
    public GameObject BarObject;

    

    private void OnMouseEnter()
    {
        EatCountFrontImage.fillAmount = (float)CurrentEatCount / MaxEat;
        BarObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        EatCountFrontImage.fillAmount = (float)CurrentEatCount / MaxEat;
        BarObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (CurrentEatCount - 1 >= 0)
        {
            CurrentEatCount--;
            EatCountFrontImage.fillAmount = (float)CurrentEatCount / MaxEat;
            GameManager.Instance.Energy += 1 * EatToEnergyScale;
            PlayerPrefs.SetInt("FrideEat",CurrentEatCount);
        }
    }

    

    private void Awake()
    {
        CurrentEatCount = PlayerPrefs.GetInt("FrideEat");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("FrideEat", CurrentEatCount);
    }
}
