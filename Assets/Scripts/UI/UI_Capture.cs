using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Capture : MonoBehaviour
{
    [field:SerializeField] private Image capture_Bar;

    private void Start()
    {
        capture_Bar.fillAmount = 0;
    }

    public void UpdateCaptureBar(float capture_percent)
    {
        capture_Bar.fillAmount = capture_percent;
    }
}
