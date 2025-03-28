using System;
using UnityEngine;

public class UI_Swipe_Room : MonoBehaviour
{

    [SerializeField] private GameObject rooms;
    [SerializeField] private int room_offset;
    [SerializeField] private int room_count;
    private int room_index = 0;

    [SerializeField] private GameObject UI_Arrow_Right;
    [SerializeField] private GameObject UI_Arrow_Left;
    

    private void Start()
    {
        UI_Arrow_Left.SetActive(false);
    }

    public void OnSwipeLeft()
    {
        Vector3 translation_offset = new Vector3(room_offset, 0, 0);
        this.rooms.transform.Translate(translation_offset);
        room_index--;
        SetArrowVisibility();
    }
    
    public void OnSwipeRight()
    {
        Vector3 translation_offset = new Vector3(-room_offset, 0, 0);
        this.rooms.transform.Translate(translation_offset);
        room_index++;
        SetArrowVisibility();
    }

    private void SetArrowVisibility()
    {
        if (room_index == 0)
        {
            UI_Arrow_Left.SetActive(false);
        }
        else
        {
            UI_Arrow_Left.SetActive(true);
        }
        
        if (room_index == room_count)
        {
            UI_Arrow_Right.SetActive(false);
        }
        else
        {
            UI_Arrow_Right.SetActive(true);
        }
    }
}
