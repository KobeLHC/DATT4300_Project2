using UnityEngine;

public class CarExplosionTester : MonoBehaviour
{
    public ParticleSystem explosionParticle; // assign your particle prefab here
    public AudioSource explosionAudio;       // optional explosion sound
    public Transform car;                    // assign your car here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (car && explosionParticle)
            {
                // Move explosion to car position
                explosionParticle.transform.position = car.position;

                // Play the particle system
                explosionParticle.Play();

                // Play sound if assigned
                if (explosionAudio) explosionAudio.Play();
            }
        }
    }
}
