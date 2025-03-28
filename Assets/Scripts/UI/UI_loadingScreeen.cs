using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_loadingScreeen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadGame()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
