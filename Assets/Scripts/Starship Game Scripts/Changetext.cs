using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Changetext : MonoBehaviour
{
    [SerializeField] Text changetext;
    [SerializeField] Race race;
    [SerializeField] LevelSpawner ls;
    // Start is called before the first frame update
    void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.RING_ENTERED, this.updatetext);
        changetext.text = "Objective " + race.getactive() + " / " + race.getlistCount() + " RacePoints";
        if(ls.getEnemyCount() > 0)
            changetext.text += "\nEnemies Remaining: " + ls.getEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {
        updatetext();
    }
    private void updatetext()
    {
        changetext.text = "Objective " + race.getactive() + " / " + race.getlistCount() + " RacePoints";
        if (ls.getEnemyCount() > 0)
            changetext.text += "\nEnemies Remaining: " + ls.getEnemyCount();
    }
}
