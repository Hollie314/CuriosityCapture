using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endUI;

    
    private void OnEnable()
    {
        GameManager.OnEndGame += ToggleEnd;
    }
    
    private void OnDisable()
    {
        GameManager.OnEndGame -= ToggleEnd;
    }
    
    public void StartOver()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ToggleEnd()
    {
        endUI.SetActive(true);
    }
}
