using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    [SerializeField] Canvas MainMenu;
    [SerializeField] GameObject Player;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Camera Seat;
    [SerializeField] Canvas PressE;
    private bool inside = false;

    private void Awake()
    {
        Time.timeScale = 1; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inside)
            {
                Activate();
                inside = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Player.name)
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == Player.name)
        {
            inside = false;
        }
    }


    private void Activate()
    {
        PressE.enabled = false;
        FPSCamera.enabled = !FPSCamera.enabled;
        GameObject.Destroy(Player);
        Seat.enabled = !Seat.enabled;
        MainMenu.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
