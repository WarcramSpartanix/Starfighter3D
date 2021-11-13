using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private int bulletspeed = 25;
    private float duration = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.up * -1 * bulletspeed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
