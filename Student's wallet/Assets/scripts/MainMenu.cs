using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsOBJ;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ChangeSettingsVisible ()
    {
        SettingsOBJ.SetActive(!SettingsOBJ.activeSelf);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
