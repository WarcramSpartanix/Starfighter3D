using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpParticle : MonoBehaviour
{
    [SerializeField] private float DefaultStarshipSpeed;
    [SerializeField] private FPS_Controller Starship;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float maxEmission = 20f;
    private float currentEmmission = 0f;

    private void Start()
    {
    }

    private void Update()
    {
        //if (particleSystem != null)
        //{

        //    UnityEngine.ParticleSystem.VelocityOverLifetimeModule mode = particleSystem.velocityOverLifetime;
        //    mode.zMultiplier = Starship.currentSpeed / DefaultStarshipSpeed * 10;

        //    UnityEngine.ParticleSystem.EmissionModule emmision = particleSystem.emission;
        //    emmision.rate = Starship.currentSpeed / DefaultStarshipSpeed * 6;
        //}

        Thrust();
    }

    private void Thrust()
    {
        if (Starship.getBoost())
        {
            UnityEngine.ParticleSystem.VelocityOverLifetimeModule mode = particleSystem.velocityOverLifetime;
            mode.zMultiplier = Starship.currentSpeed / DefaultStarshipSpeed * 20;

            UnityEngine.ParticleSystem.EmissionModule emmision = particleSystem.emission;
            emmision.rateOverTime = maxEmission;
            currentEmmission = maxEmission;
        }
        else
        {
            UnityEngine.ParticleSystem.EmissionModule emmision = particleSystem.emission;
            if (currentEmmission > 0)
            {
                currentEmmission -= maxEmission * Time.deltaTime;
                if (currentEmmission < 0)
                    currentEmmission = 0;
                emmision.rateOverTime = currentEmmission;
            }
        }
    }
}
