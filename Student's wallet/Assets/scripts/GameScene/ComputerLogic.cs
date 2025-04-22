using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerLogic : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
