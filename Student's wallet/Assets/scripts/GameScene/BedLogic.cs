using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedLogic : MonoBehaviour
{
    [Header("UI")]
    public GameObject BedPanel;
    public Image SleepImage;

    [Header("Configurations")]
    public List<int> SleepAcceptableTime;
    public GameCycle GC;

    public bool Sleep { get; private set; }

    public void Start()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
    }
    private void OnMouseDown()
    {
        BedPanel.SetActive(true);
        
    }

    void MakeSleep()
    {
        if (SleepAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            Debug.Log("U can sleep");
            GameManager.Instance.ChangeTime(1, 0);
            GameManager.Instance.Energy += 10;
            GameManager.Instance.Psycho += 2;
            GC.FadeScreen(2,1);            
            Sleep = true; 
          
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
}