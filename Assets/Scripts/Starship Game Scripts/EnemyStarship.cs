using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarship : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject player;
    [SerializeField] private List<EnemyBlaster> blasters = new List<EnemyBlaster>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    private void Movement()
    {
        if (player != null)
        {
            var rotation = Quaternion.LookRotation(player.transform.position - transform.position) * Quaternion.Euler(0, 90, 0);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            transform.position += transform.right * -1 * speed * Time.deltaTime;
        }
    }

    public void SetTarget(GameObject player)
    {
        this.player = player;
    }
}
