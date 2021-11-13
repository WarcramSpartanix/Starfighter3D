using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseManager.getPause())
        {
            PauseGame();
        }
    }

     public void PauseGame()
    {
        pauseManager.PauseGame(CANVAS_NAMES.PAUSE_UI);
    }
}
