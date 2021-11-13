using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;
    [SerializeField] private AudioSource AudioExplosion;
    [SerializeField] private PostProcessProfile ppf;
    [SerializeField] private int HP;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas gameover;
    [SerializeField] private PauseManager pauseManager;
    private bool coolided = false;
    private bool died = false;

    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.name.Contains("Race Point"))
            HP -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Race Point"))
            HP -= 1;
    }

    private void OnCollisionExit(Collision collision)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //EventBroadcaster.Instance.AddObserver(EventNames.StarFighter.RING_ENTERED, this.addhp);
        healthSlider.maxValue = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && !died)
        {
            Debug.Log(HP);

            //Destroy(this.gameObject, 5);


            this.GetComponentInChildren<FPS_Controller>().Disable();


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
                collider.enabled = false;
            }

            this.enabled = false;

            if (AudioExplosion != null)
            {
                AudioExplosion.Play();
            }
            Destroy(GameObject.Instantiate(Explosion, this.transform), 5);

            died = true;

            Invoke("GameOver", 2);
            
        }

        ActivateVignette();
        SetHealth();
    }

    private void ActivateVignette()
    {
        if (HP < 20)
        {
            ppf.GetSetting<Vignette>().intensity.value = 1.0f;
        }
        else
        {
            ppf.GetSetting<Vignette>().intensity.value = 0.0f;
        }
    }

    private void SetHealth()
    {
        healthSlider.value = HP;
    }

    public bool Alive()
    {
        return HP > 0;
    }

    private void GameOver()
    {
        pauseManager.PauseGame(CANVAS_NAMES.GAME_OVER_UI);
    }
    private void addhp()
    {
        HP++;
    }
}
