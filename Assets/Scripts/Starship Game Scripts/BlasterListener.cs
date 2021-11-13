using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterListener : MonoBehaviour
{
    [SerializeField] private GameObject Laser;
    [SerializeField] private List<GameObject> LaserList = new List<GameObject>();
    private float ShootCooldown = 0.2f;
    private float Timer = 0.0f;
    [SerializeField] AudioSource shootSound;
    private bool Shooting = false;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.ON_FIRE_FIRE_BLASTER, this.StartShooting);
        EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.ON_FIRE_STOP_BLASTER, this.StopShooting);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.StarFighter.ON_FIRE_FIRE_BLASTER);
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (!this.GetComponentInParent<PlayerShip>().Alive())
        {
            Shooting = false;
        }

        if (Shooting)
        {
            Shoot();
        }
    }

    private void StartShooting()
    {
        Shooting = true;
    }

    private void StopShooting()
    {
        Shooting = false;
    }

    private void Shoot()
    {
        if (Timer <= 0)
        {
            Timer = ShootCooldown;
            if (shootSound != null)
            {
                shootSound.Play();
            }

            GameObject temp = GameObject.Instantiate(Laser);

            temp.transform.position = this.transform.position;
            temp.transform.rotation = this.transform.rotation;
            temp.transform.Rotate(new Vector3(0, 0, 1), -90);

            temp.transform.position += this.transform.right * -1 * 0.60f;
            temp.AddComponent<BulletMovement>();

            LaserList.Add(temp);
        }
    }

    private void DeleteObjects()
    {
        foreach (GameObject obj in LaserList)
        {
            GameObject.Destroy(obj);
        }
    }
}
