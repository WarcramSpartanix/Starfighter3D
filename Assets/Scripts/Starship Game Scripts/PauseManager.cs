using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private Canvas readyUI;
    [SerializeField] private Canvas pauseUI;
    [SerializeField] private Canvas gameOverUI;
    [SerializeField] private Canvas winCanvas;
  
    private void Awake()
    {
        HideAllCanvas();
        this.PauseGame(CANVAS_NAMES.READY_UI);
    }

    private void Update()
    {
        if (paused && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void PauseGame(string canvas = null)
    {
        if (!paused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ShowCanvas(canvas);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //HideCanvas(canvas);
            HideAllCanvas();
        }

        paused = !paused;

    }

    public void ShowCanvas(string canvas)
    {
        if (string.Compare(canvas, CANVAS_NAMES.READY_UI) == 0)
        {
            readyUI.enabled = true;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.PAUSE_UI) == 0)
        {
            pauseUI.enabled = true;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.GAME_OVER_UI) == 0)
        {
            gameOverUI.enabled = true;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.WIN_CANVAS) == 0)
        {
            winCanvas.enabled = true;
        }
    }

    public void HideCanvas(string canvas)
    {
        if (string.Compare(canvas, CANVAS_NAMES.READY_UI) == 0)
        {
            readyUI.enabled = false;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.PAUSE_UI) == 0)
        {
            pauseUI.enabled = false;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.GAME_OVER_UI) == 0)
        {
            gameOverUI.enabled = false;
        }
        else if (string.Compare(canvas, CANVAS_NAMES.WIN_CANVAS) == 0)
        {
            winCanvas.enabled = false;
        }
    }

    public void HideAllCanvas()
    {
        readyUI.enabled = false;
        pauseUI.enabled = false;
        gameOverUI.enabled = false;
        winCanvas.enabled = false;
    }

    public bool getPause()
    {
        return paused;
    }


    
}

public class CANVAS_NAMES
{
    public const string READY_UI = "READY_UI";
    public const string PAUSE_UI = "PAUSE_UI";
    public const string GAME_OVER_UI = "GAME_OVER_UI";
    public const string WIN_CANVAS = "WIN_CANVAS";
}