using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlaster : MonoBehaviour
{

    [SerializeField] GameObject Laser;
    [SerializeField] List<GameObject> LaserList = new List<GameObject>();
    private float ShootCooldown = 0.5f;
    private float Timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Laser != null && this.GetComponentInParent<EnemyShip>().Alive())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Timer <= 0)
        {
            Timer = ShootCooldown;

            GameObject temp = GameObject.Instantiate(Laser);

            temp.transform.position = this.transform.position;
            temp.transform.rotation = this.transform.rotation;
            temp.transform.Rotate(new Vector3(0, 0, 1), -90);

            temp.transform.position += this.transform.right * -1 * 0.60f;
            temp.AddComponent<BulletMovement>();

            LaserList.Add(temp);
        }
    }
}
