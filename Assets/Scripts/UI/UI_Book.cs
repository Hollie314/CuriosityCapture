using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void InitUIdata(Curiosity_Data[] data)
    {
        for (int i=0; i < data.Length; i++)
        {
            curiosity_tamponUI[i].transform.Find("titre").GetComponent<TextMeshPro>().text = data[i].Curiosity_name;
            curiosity_tamponUI[i].transform.Find("description").GetComponent<TextMeshPro>().text = data[i].Curiosity_description;
            curiosity_tamponUI[i].transform.Find("icone").GetComponent<Image>().sprite = data[i].Curiosity_icone;
        }
    }

    public void ToogleUI()
    {
        book_UI.SetActive(!book_UI.activeSelf);
        Cursor.visible = !Cursor.visible;
    }
}
