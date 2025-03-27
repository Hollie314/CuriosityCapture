using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Update()
        {
            // Get mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = 0; // Set Z to 0 for 2D
    
            // Move object to mouse position
            transform.position = mousePos;
        }
}
