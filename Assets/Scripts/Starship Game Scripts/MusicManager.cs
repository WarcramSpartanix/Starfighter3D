using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio1;

    [SerializeField] private AudioSource audio2;
    [SerializeField] private LevelSpawner ls;
    // Start is called before the first frame update
    void Start()
    {
        audio1.GetComponent<AudioSource>();
        audio1.Play(0);
        audio2.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(ls.getEnemyCount() > 1 && !audio2.isPlaying)
        {
            audio1.Stop();
            audio2.Play(0);
        }
    }
}
