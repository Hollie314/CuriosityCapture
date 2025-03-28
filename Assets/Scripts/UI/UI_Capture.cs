using System;
using UnityEngine;

public class UI_Capture : MonoBehaviour
{
    
    [SerializeField] private GameObject[] captured_Curiosities;

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
        captured_Curiosities[index].SetActive(true);
    }
}
