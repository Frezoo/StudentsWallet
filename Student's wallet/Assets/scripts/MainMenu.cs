using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsOBJ;


    //private void Start()
    //{
    //    PlayerPrefs.DeleteAll(); // Очищаем все PlayerPrefs
    //    PlayerPrefs.Save();
    //}
    public void ChangeSettingsVisible ()
    {
        SettingsOBJ.SetActive(!SettingsOBJ.activeSelf);
    }

    public void StartGame()
    {
        // Загружаем сцену NewGameSetupScene, если игра запускается впервые
        // или GameScene если игра уже запускалась.

        if (PlayerPrefs.GetInt("GameInitialized", 0) == 0)
        {
            // Загружаем сцену NewGameSetupScene
            SceneManager.LoadScene("NewGameSetupScene");
        }
        else
        {
            // Загружаем сцену GameScene
            SceneManager.LoadScene("GameScene");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
