using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBroadcaster : MonoBehaviour
{
    private bool entered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (entered == false)
        {
            EventBroadcaster.Instance.PostEvent("RING_ENTERED");
            entered = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
