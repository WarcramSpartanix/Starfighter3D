using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField] private Pause pause;

    public void OnButtonPress()
    {
        pause.PauseGame();
    }
}
