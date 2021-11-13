using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float Radius;
    [SerializeField] private Vector3 Center;
    private float CurrentRevolutionAngle;
    [SerializeField] private float RevolutionSpeed;

    private void Start()
    {
        CurrentRevolutionAngle = UnityEngine.Random.Range(0, 360);

        this.transform.position = new Vector3((float) Math.Cos(CurrentRevolutionAngle) * Radius, 0, (float)Math.Sin(CurrentRevolutionAngle) * Radius);
    }

    private void Update()
    {
        CurrentRevolutionAngle += RevolutionSpeed * Time.deltaTime;
        this.transform.position = new Vector3((float)Math.Cos(CurrentRevolutionAngle) * Radius, 0, (float)Math.Sin(CurrentRevolutionAngle) * Radius);
        this.transform.Rotate(new Vector3(0, 0, 0.1f * Time.deltaTime));
    }
}
