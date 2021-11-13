using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour
{
    [SerializeField] List<GameObject> ringlist;
    [SerializeField] AudioSource sfx;
    private int active = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < ringlist.Count; i++)
        {
            ringlist[i].SetActive(false);
        }

        EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.RING_ENTERED, this.ChangeRings);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.StarFighter.RING_ENTERED);
    }

    // Update is called once per frame
    void ChangeRings()
    {
        ringlist[active].SetActive(false);
        if (active < ringlist.Count)
        {
            active++;
        }
        if(active == ringlist.Count)
        {
            Debug.Log("enter final");
            EventBroadcaster.Instance.PostEvent("FINAL_RING_CROSSED");
        }
        if (active < ringlist.Count)
        {
            ringlist[active].SetActive(true);
        }
        PlaySound();
    }

    void PlaySound()
    {
        if (sfx != null)
        {
            sfx.Play();
        }
    }
    public int getactive()
    {
        return active;
    }
    public int getlistCount()
    {
        return ringlist.Count;
    }
}
