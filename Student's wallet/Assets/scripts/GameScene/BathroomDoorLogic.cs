using UnityEngine;
using System.Collections.Generic;
public class BathroomDoorLogic : MonoBehaviour
{
    [Header("Configurations")]
    public List<int> BrsuhAcceptableTime;
    public List<int> ShowerAcceptableTime;
    public int TimeToBrushInMinuts;
    public int TimeToShowerInMinuts;
    public GameCycle GC;

    [Header("UI")]
    public GameObject BathroomPanel;

    private void Start()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
    }

    public void BrushTeeth()
    {
        if (BrsuhAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.Psycho += 5;
            GC.FadeScreen(TimeToBrushInMinuts / 60 * 2, 0.4f);
            GameManager.Instance.ChangeTime(0, TimeToBrushInMinuts);
            
        }
    }

    public void TakeAShower()
    {
        if (ShowerAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.Psycho += 15;
            GC.FadeScreen(TimeToBrushInMinuts / 60 * 2, 0.4f);
            GameManager.Instance.ChangeTime(0, TimeToShowerInMinuts);
        }

        
    }

    public void ClosePanel()
    {
        BathroomPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        BathroomPanel.SetActive(true);
    }
}
