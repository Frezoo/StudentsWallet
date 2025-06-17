using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DoorOutLogic : MonoBehaviour
{
    public GameObject DoorInterface;
    public GameCycle GC;

    public List<int> AcceptedWorkTime;
    public List<int> AcceptedGymTime;
    public List<int> AcceptedMarketTime;
    public List<int> AcceptedUniversityTime;

    

    void Start()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
    }

    public void GoToWork()
    {
        if (AcceptedWorkTime.Contains(GameManager.Instance.Hourse))
        {
            if (GameManager.Instance.Job == 0)
            {
                return;
            }
            GC.FadeScreen(6, 0.4f);
            GameManager.Instance.ChangeTime(3, 0);
            GameManager.Instance.Energy -= 50;
            GameManager.Instance.Psycho -= 50;

            SceneManager.LoadSceneAsync(4 + (int)GameManager.Instance.Job);
        }
       
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
        if (AcceptedUniversityTime.Contains(GameManager.Instance.Hourse) && !(GameManager.Instance.IsPlayerWasOnLecture))
        {
            GameManager.Instance.ChangeTime(3, 0);
            GameManager.Instance.Energy -= 55;
            GameManager.Instance.Psycho -= 20;
            GameManager.Instance.IsPlayerWasOnLecture = true;
            if (GameManager.Instance.Day % 2 == 0)
            {
                GC.FadeScreen(6, 1);
            }
            else if (GameManager.Instance.Day == 1)
            {
                SceneManager.LoadScene(8);
                PlayerPrefs.SetInt("LectureNumber", 1);
            }
            else if(GameManager.Instance.Day <= 10)
            {
                SceneManager.LoadScene(8);
                PlayerPrefs.SetInt("LectureNumber", 1 + (GameManager.Instance.Day / 2));
            }
            else
            {
                GC.FadeScreen(6, 1);
            }
            
        }


    }

    public void GoToGym()
    {
        if (AcceptedGymTime.Contains(GameManager.Instance.Hourse))
        {
            GameManager.Instance.ChangeTime(1, 0);
            GC.FadeScreen(4, 2);
            GameManager.Instance.Energy -= 30;
            GameManager.Instance.Psycho += 30;
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
