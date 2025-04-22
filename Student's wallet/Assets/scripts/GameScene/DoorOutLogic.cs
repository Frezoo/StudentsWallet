using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOutLogic : MonoBehaviour
{
    public GameObject DoorInterface;
    public GameCycle GC;

    public List<int> AcceptedWorkTime;
    public List<int> AcceptedGymTime;
    public List<int> AcceptedMarketTime;
    public List<int> AcceptedUniversityTime;

    private void Awake()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
    }

    public void GoToWork()
    {

    }

    public void GoToMarket()
    {
        if (AcceptedMarketTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.ChangeTime(0, 40);
            GC.FadeScreen(1.33f, 0.1f);
            SceneManager.LoadSceneAsync(3);
        }
        
    }

    public void GoToUniversity()
    {
        if (AcceptedUniversityTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.ChangeTime(3, 0);
            GC.FadeScreen(6, 1);
            GameManager.Instance.Energy -= 55;
            GameManager.Instance.Psycho -= 20;
        }


    }

    public void GoToGym()
    {
        if (AcceptedGymTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.ChangeTime(1, 0);
            GC.FadeScreen(2, 1);
            GameManager.Instance.Energy -= 30;
            GameManager.Instance.Psycho -= 30;
        }
        
    }

    public void ClosePanel()
    {
        DoorInterface.SetActive(false);
    }

    private void OnMouseDown()
    {
        DoorInterface.SetActive(true);
    }
}
