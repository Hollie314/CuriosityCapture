using System.Collections.Generic;
using TMPro;
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

    public void InitUIdata(int index, Curiosity_Data data)
    {
        //curiosity_tamponUI[index].transform.Find()
        curiosity_tamponUI[index].GetComponentInChildren<TextMeshPro>().text = data.Curiosity_description;
    }

    public void ToogleUI()
    {
        book_UI.SetActive(!book_UI.activeSelf);
    }
}
