using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;

    public void OnButtonPress()
    {
        pauseManager.PauseGame();
    }
}
