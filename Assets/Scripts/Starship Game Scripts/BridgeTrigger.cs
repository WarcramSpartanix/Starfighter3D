using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private bool enable = false;

    private void Awake()
    {
    }

    private void Update()
    {
        canvas.enabled = enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        enable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        enable = false;
    }
}
