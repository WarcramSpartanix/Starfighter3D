using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Starship;
    [SerializeField] private List<SphereCollider> PlanetList = new List<SphereCollider>();
    [SerializeField] private List<GameObject> MeteorSwarmPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> MeteorSwarms = new List<GameObject>();
    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private float Interval;  // Seconds
    [SerializeField] private int MeteorSwarmLimit;
    [SerializeField] private int PrewarmCount;
    [SerializeField] private float SwarmGap;
    [SerializeField] private float MaxDist;
    [SerializeField] private float MinDist;
    [SerializeField] private float MeteorDespawnDist;
    [SerializeField] private float EnemySpawnTime;
    [SerializeField] private int EnemySpawnCount;
    private RangeInt spawnTimeRange = new RangeInt(30,45);
    public bool isActive = false;

    private float TimePassed = 0.0f;
    private float EnemyTimePassed = 0f;
    private bool winRound = false;
    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.FINAL_RING_CROSSED, this.setactive);
        for (int i = 0; i < PrewarmCount; i++)
        {
            SpawnMeteor();
        }

        isActive = false;
    }
    private void Update()
    {
        
        TimePassed += Time.deltaTime;
        EnemyTimePassed += Time.deltaTime;

        while (TimePassed > Interval)
        {
            TimePassed -= Interval;
            SpawnMeteor();
        }

        if (EnemyTimePassed > UnityEngine.Random.Range(spawnTimeRange.start, spawnTimeRange.end))
        {
            SpawnEnemies(1);
            EnemyTimePassed = 0;
        }


        ClearMeteorByDistance();

        RemoveNull();
        CheckWin();
        //if (isActive)
        //{
        //    EnemyTimePassed += Time.deltaTime;
        //    if (!float.IsNaN(EnemyTimePassed) && EnemyTimePassed >= EnemySpawnTime)
        //    {
        //        EnemyTimePassed = float.NaN;
        //        SpawnEnemies();
        //    }
        //}
    }

    private void SpawnEnemies(int enemyCount = 0)
    {
        if (enemyCount == 0)
        {
            enemyCount = EnemySpawnCount;
        }
        for(int i =0; i< enemyCount; i++)
        {
            //bool spawned = false;
            //do
            //{

                GameObject temp = Instantiate(EnemyPrefab);
                temp.GetComponent<EnemyStarship>().SetTarget(this.Starship);

                Vector3 displacement = new Vector3(RandomDist(), RandomDist(), RandomDist());

                temp.transform.position = Starship.transform.position + displacement;
                temp.transform.Rotate(new Vector3(Random.Range(0f, 360.0f), Random.Range(0f, 360.0f), Random.Range(0f, 360.0f)));

                //if (!CheckInside(temp.transform.position))
                //{
                    //spawned = true;
                    EnemyList.Add(temp);
                //}
                //else
                //{
                //    GameObject.Destroy(temp);
                //}
            //} while (!spawned);
        }
    }

    private bool CheckNearShip(Vector3 position)
    {
        if (Vector3.Distance(Starship.transform.position, position) < 10)
        {
            return true;
        }
        return false;
    }

    private void SpawnMeteor()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        if (MeteorSwarms.Count < MeteorSwarmLimit && Starship != null)
        {
            bool spawned = false;

            do
            {

                GameObject temp = GameObject.Instantiate(MeteorSwarmPrefab[rand]);

                Vector3 displacement = new Vector3(RandomDist(), RandomDist(), RandomDist());
                temp.transform.position = Starship.transform.position + displacement;
                temp.transform.Rotate(new Vector3(Random.Range(0f,360.0f), Random.Range(0f, 360.0f), Random.Range(0f, 360.0f)));

                if (!CheckInside(temp.transform.position))
                {
                    spawned = true;
                    MeteorSwarms.Add(temp);
                }
                else
                {
                    GameObject.Destroy(temp);
                }
            } while (!spawned);
        }
    }

    private void ClearMeteorByDistance()
    {
        GameObject toRemove = null;

        foreach (GameObject meteorswarm in MeteorSwarms)
        {
            if (Vector3.Distance(meteorswarm.transform.position, Starship.transform.position) >= MeteorDespawnDist)
            {
                // to avoid error on collection changes
                toRemove = meteorswarm;
                break;
            }
        }
        MeteorSwarms.Remove(toRemove);
        Destroy(toRemove);
    }

    private float RandomDist()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        int multiplier;
        if (rand % 2 == 0)
            multiplier = 1;
        else
            multiplier = -1;


        return UnityEngine.Random.Range(MinDist, MaxDist) * multiplier;
    }

    private bool CheckInside(Vector3 position)
    {
        foreach (SphereCollider collider in PlanetList)
        {
            float distance = Vector3.Distance(position, collider.transform.position);

            if (distance < collider.radius)
            {
                return true;
            }
        }

        return false;
    }
    public void setactive()
    {
        winRound = true;
        SpawnEnemies();
    }
    
    public int getEnemyCount()
    {
        return EnemyList.Count;
    }

    public void CheckWin()
    {
        if (winRound && EnemyList.Count == 0 && !pauseManager.getPause())
        {
            pauseManager.PauseGame(CANVAS_NAMES.WIN_CANVAS);
        }
    }

    public void RemoveNull()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i] == null)
            {
                EnemyList.RemoveAt(i);
                break;
            }
        }
    }
}
