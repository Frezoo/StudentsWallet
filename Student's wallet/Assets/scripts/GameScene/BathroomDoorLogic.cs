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

    [Header("Audio")]
    public AudioClip TakeaShowerAudio;
    public AudioClip BrushTeethsAudio;

    private void Start()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
    }

    public void BrushTeeth()
    {
        if (BrsuhAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.Psycho += 5;
            GC.FadeScreen(3, 0.4f);
            GameManager.Instance.ChangeTime(0, TimeToBrushInMinuts);
            GameManager.Instance.PlayActionAudio(BrushTeethsAudio);


        }
    }

    public void TakeAShower()
    {
        if (ShowerAcceptableTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.Psycho += 5;
            GC.FadeScreen(11, 0.4f);
            GameManager.Instance.ChangeTime(0, TimeToShowerInMinuts);
            GameManager.Instance.PlayActionAudio(TakeaShowerAudio);
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
