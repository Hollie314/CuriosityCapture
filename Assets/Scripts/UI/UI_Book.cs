using System.Collections.Generic;
using UnityEngine;

public class UI_Book : MonoBehaviour
{

    [SerializeField] private GameObject book_UI;
    [SerializeField] private GameObject[] curiosity_tamponUI;

    
    private void OnEnable()
    {
        GameManager.OnCapture += UpdateCuriosityUI;
    }
    
    private void OnDisable()
    {
        GameManager.OnCapture -= UpdateCuriosityUI;
    }
    
    public void UpdateCuriosityUI(int index)
    {
        curiosity_tamponUI[index].SetActive(true);
    }

    public void ToogleUI()
    {
        book_UI.SetActive(!book_UI.activeSelf);
    }
}
