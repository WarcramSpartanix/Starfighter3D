using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;
    [SerializeField] private AudioSource AudioExplosion;
    private int hp = 8;

    void OnTriggerEnter(Collider other)
    {
         hp -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hp -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Debug.Log(hp);
            
            Destroy(this.gameObject, 2);


            MeshRenderer[] meshes = this.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
            
            ParticleSystem[] particleSystems = this.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                Destroy(particleSystem);
            }

            Collider[] colliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false ;
            }

            this.enabled = false;
            
            if (AudioExplosion != null)
            {
                AudioExplosion.Play();
            }
            Destroy(GameObject.Instantiate(Explosion, this.transform), 2);
        }
    }
    public bool Alive()
    {
        return hp > 0;
    }
}
