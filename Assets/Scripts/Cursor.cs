using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Update()
        {
            // Get mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Set Z to 0 for 2D
    
            // Move object to mouse position
            transform.position = mousePos;
        }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Curiosity"))
        {
            GameManager.GetInstance().CaptureBegin(other.GetComponent<Curiosity>());
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Curiosity"))
        {
            GameManager.GetInstance().CaptureEnd(other.GetComponent<Curiosity>());
        }
    }
    
}
